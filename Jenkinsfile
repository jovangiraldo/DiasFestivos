pipeline {
    agent any

    environment {
        COMPOSE_FILE = 'docker-compose.yml'
        DOCKER_IMAGE = 'mssql-sqlserver:latest'
        CONTAINER_NAME = 'dockerbdfestivos'
        HOST_PORT = '1433'
        APP_PORT = '1433'
        DOCKER_NETWORK = 'mssql_red'
    }

    stages {

        stage('Clonar código') {
            steps {
                git url: 'https://github.com/jovangiraldo/DiasFestivos.git', branch: 'main'
            }
        }

        stage('Construir contenedores') {
            steps {
                bat 'docker compose build'
            }
        }

        stage('Detener contenedores previos (opcional)') {
            steps {
                bat 'docker compose down || exit 0'
            }
        }

        stage('Levantar servicios') {
            steps {
                bat 'docker ps -a -q -f name=apifestivos | findstr . && docker rm -f apifestivos || echo "No existing container"'
                bat 'docker compose up -d'
            }
        }

        stage('Verificar que SQL está corriendo') {
            steps {
                bat 'docker ps'
                // También puedes verificar por nombre o puerto:
                // bat 'docker inspect dockerbdfestivos'
            }
        }

        stage('Pruebas (opcional)') {
            steps {
                echo 'Aquí podrías ejecutar pruebas unitarias o de integración si las tienes.'
                // bat 'dotnet test ./ApiFestivos.test/ApiFestivos.test.csproj'
            }
        }
    }

    post {
        always {
            echo 'Pipeline terminado.'
        }
    }
}
