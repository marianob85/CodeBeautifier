properties(
	[
		buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '', numToKeepStr: '10')),
		pipelineTriggers([pollSCM('0 H(5-6) * * *')])
	]
)

pipeline
{
	agent any
	options {
		skipDefaultCheckout true
	}
	environment {
		GITHUB_TOKEN = credentials('marianob85-github-jenkins')
	}
	stages
	{
		stage('Build'){
			agent{ label "windows/buildtools2019" }
			steps {
				checkout scm
				script {
					env.GITHUB_REPO = sh(script: 'basename $(git remote get-url origin) .git', returnStdout: true).trim()
				}
				powershell './BuildScripts/InjectGitVersion.ps1 -Version $env:BUILD_NUMBER'
				bat '''
					call "C:/BuildTools/VC/Auxiliary/Build/vcvars64.bat"
					nuget restore CodeBeautifier.sln
					msbuild CodeBeautifier.sln /t:Rebuild /p:Configuration=Release;Platform="Any CPU" /flp:logfile=warnings.log;warningsonly
					'''
				stash includes: "warnings.log", name: "warningsFiles"
				stash includes: 'Installers/*, CodeBeautifier-VSPackage/out/Release/*.vsix', name: "bin"
				stash includes: 'UnitTest/bin/Release/*', name: "unitTest"
			}
		}
		stage('UnitTests'){
			agent{ label "windows/buildtools2019" }
			steps {
				unstash "unitTest"
				powershell '''
					vstest.console.exe UnitTest/bin/Release/UnitTest.dll /Logger:trx
				'''
				mstest testResultsFile:"**/*.trx", keepLongStdio: true
			}
		}
		stage('Compile check'){
			agent any
			steps {
				unstash "warningsFiles"
				script {
					def warn = scanForIssues sourceCodeEncoding: 'UTF-8', tool: msBuild(id: 'msvc', pattern: 'warnings.log')
					publishIssues failedTotalAll: 1, issues: [warn], name: 'Win compilation warnings'	
				}
			}
		}
		
		stage('Archive'){
			agent any
			steps {
				unstash "bin"
				archiveArtifacts artifacts: 'Installers/*, CodeBeautifier-VSPackage/out/Release/*.vsix', onlyIfSuccessful: true
			}
		}
		
		stage('Release') {
			when {
				buildingTag()
			}
			agent{ label "linux/u18.04/go:1.17.3" }
			steps {
				unstash 'bin'
				sh '''
					go install github.com/github-release/github-release@v0.10.0
					github-release release --user marianob85 --repo ${GITHUB_REPO} --tag ${TAG_NAME} --name ${TAG_NAME}
					for filename in CodeBeautifier-VSPackage/out/Release/*.vsix; do
						[ -e "$filename" ] || continue
						basefilename=$(basename "$filename")
						github-release upload --user marianob85 --repo ${GITHUB_REPO} --tag ${TAG_NAME} --name ${basefilename} --file ${filename}
					done
					for filename in Installers/*.zip; do
						[ -e "$filename" ] || continue
						basefilename=$(basename "$filename")
						github-release upload --user marianob85 --repo ${GITHUB_REPO} --tag ${TAG_NAME} --name ${basefilename} --file ${filename}
					done
				'''
			}
		}
	}
	post { 
        changed { 
            emailext body: 'Please go to ${env.BUILD_URL}', to: '${DEFAULT_RECIPIENTS}', subject: "Job ${env.JOB_NAME} (${env.BUILD_NUMBER}) ${currentBuild.currentResult}".replaceAll("%2F", "/")
        }
    }
}