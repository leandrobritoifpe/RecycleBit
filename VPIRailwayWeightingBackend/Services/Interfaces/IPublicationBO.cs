using System;
using System.Collections.Generic;
using System.Net.Http;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     The publication business object interface.
    /// </summary>
    public interface IPublicationBO {
        void CreatePublication(object publication);
        void EditPublication(object publication);
        void CancelPublication(int idPublication);
        void getAllPublication();
        void getPublicationById();
    }
}