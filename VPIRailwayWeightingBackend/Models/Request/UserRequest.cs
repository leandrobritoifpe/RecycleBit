using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VPIRailwayWeightingBackend.Models.Request {
    public class UserRequest {
        public int Id { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public string Perfil { get; set; }
        public string Phone { get; set; }
        public DateTime DateNasc { get; set; }
        public string CPF { get; set; }
    }
}