properties(
	[
		buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '', numToKeepStr: '10')),
		pipelineTriggers([pollSCM('0 H(5-6) * * *')])
	]
)

pipeline
{
	agent { 
		node { 
			label 'windows10x64 && development' 
		}
	}
	options {
		skipDefaultCheckout true
	}
	stages
	{
		stage('Build'){
			steps {
				dir('build') {
					checkout scm
					powershell './BuildScripts/InjectGitVersion.ps1 -Version $env:BUILD_NUMBER'
					bat '''
						call "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Auxiliary\\Build\\vcvars64.bat"
						nuget restore CodeBeautifier.sln
						msbuild CodeBeautifier.sln /t:Rebuild /p:Configuration=Release;Platform="Any CPU" /flp:logfile=warnings.log;warningsonly
						'''
					stash includes: "warnings.log", name: "warningsFiles"
					stash includes: 'Installers/*, CodeBeautifier-VSPackage/out/Release/*.vsix', name: "bin"
				}
			}
			post {   
				cleanup {
					cleanWs deleteDirs: true
				}
			}
		}
		stage('Compile check'){
			steps {
				dir('compile_check') {
					unstash "warningsFiles"
					script {
						def warn = scanForIssues sourceCodeEncoding: 'UTF-8', tool: msBuild(id: 'msvc', pattern: 'warnings.log')
						publishIssues failedTotalAll: 1, issues: [warn], name: 'Win compilation warnings'	
					}
				}
			}
			post {   
				cleanup {
					cleanWs deleteDirs: true
				}
			}
		}
		
		stage('Archive'){
			steps {
				dir('artifacts') {
					unstash "bin"
					archiveArtifacts artifacts: 'Installers/*, CodeBeautifier-VSPackage/out/Release/*.vsix', onlyIfSuccessful: true
				}
			}
			post {   
				cleanup {
					cleanWs deleteDirs: true
				}
			}
		}
	}
	post { 
        failure { 
            notifyFailed()
        }
		success { 
            notifySuccessful()
        }
		unstable { 
            notifyFailed()
        }
    }
}


def notifySuccessful() {
	echo 'Sending e-mail'
	mail (to: 'notifier@manobit.com',
         subject: "Job '${env.JOB_NAME}' (${env.BUILD_NUMBER}) success build",
         body: "Please go to ${env.BUILD_URL}.");
}

def notifyFailed() {
	echo 'Sending e-mail'
	mail (to: 'notifier@manobit.com',
         subject: "Job '${env.JOB_NAME}' (${env.BUILD_NUMBER}) failure",
         body: "Please go to ${env.BUILD_URL}.");
}