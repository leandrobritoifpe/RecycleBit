using System.Collections.Generic;

namespace RecycleBitBackEnd.Util.AutoFacModules {
    public class NameSpacesMap {
        public List<string> MappingInterfaces { get; set; }

        public List<string> MappingImplementation { get; set; }

        public List<string> MappingControllers { get; set; }

        public List<string> MappingApiControllers { get; set; }

        public NameSpacesMap() {
        }

        public NameSpacesMap(List<string> intercafes, List<string> implemantation, List<string> controllers, List<string> mappingApiControllers) {
            MappingImplementation = implemantation;
            MappingControllers = controllers;
            MappingInterfaces = intercafes;
            MappingApiControllers = mappingApiControllers;
        }
    }
}