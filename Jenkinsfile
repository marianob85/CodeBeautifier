properties(
	[
		buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '', numToKeepStr: '10')),
		pipelineTriggers([pollSCM('0 H(5-6) * * *')])
	]
)

pipeline
{
	agent { node { label 'windows10x64 && development' } }
	stages
	{
		stage('Build'){
			steps {
				bat '''
					//call "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\Tools\\VsMSBuildCmd.bat"
					call "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Auxiliary\\Build\\vcvars64.bat"
					msbuild CodeBeautifier.sln /t:Rebuild /p:Configuration=Release;Platform="Any CPU" /flp:logfile=warnings.log;warningsonly'''
			}
		}
		stage('Compile check'){
			steps {
				warnings canComputeNew: false, canResolveRelativePaths: false, defaultEncoding: '', excludePattern: '', healthy: '', includePattern: '', messagesPattern: '', parserConfigurations: [[parserName: 'MSBuild', pattern: 'warnings.log']], unHealthy: ''
			}
		}
		
		stage('Archive'){
			steps {
				archiveArtifacts artifacts: 'Installers/*', onlyIfSuccessful: true
			}
		}
		
		stage('CleanUp'){
			steps {
				deleteDir()
			}
		}
	}
}
