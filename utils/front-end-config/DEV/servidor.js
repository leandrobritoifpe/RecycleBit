//registering 'servidorConfig' in module 'app.configuration'
(
    function (module) {
        module.constant(
            'servidorConfig',
            {
                url: "https://vpi-qa.valenet.valeglobal.net/pesagembackend-dev/api/",
                host: "localhost",
                port: 10016,
                batchPort: 10016,
                adsa: "CHARINT=N;CHARFLOAT=N;CHARTIME=Y;CONVERTERRORS=N"
            }
        );
    }
)
    (angular.module('app.configuration'));