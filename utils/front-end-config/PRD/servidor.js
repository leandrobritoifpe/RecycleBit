//registering 'servidorConfig' in module 'app.configuration'
(
    function (module) {
        module.constant(
            'servidorConfig',
            {
                //url: "https://localhost:44380/api/",
                url: "https://vpi.valenet.valeglobal.net/vpi-railwayweightingbackend/api/",
                //url: "https://localhost:44380/api/",
                host: "localhost",
                port: 10016,
                batchPort: 10016,
                adsa: "CHARINT=N;CHARFLOAT=N;CHARTIME=Y;CONVERTERRORS=N"
            }
        );
    }
)
    (angular.module('app.configuration'));