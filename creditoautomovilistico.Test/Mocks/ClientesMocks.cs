using creditoautomovilistico.API.Models;
//using creditoautomovilistico.Entities;
using creditoautomovilistico.Entities.Enumerations;
using creditoautomovilistico.Infrastructure.Models;
using System;

namespace creditoautomovilistico.Test.Mocks
{
    public static class ClientesMocks
    {
        public static ClienteResponseModel GetMockedCliente(string identificacion)
        {
            return new ClienteResponseModel()
            {
                Id = 1,
                Identificacion = identificacion,
                EstadoCivil = (TipoEstadoCivil)1,
                IdentificacionConyuge = "1234567890",
                NombreConyuge = "Jane Doe",
                SujetoDeCredito = true,
                Nombres = "John F.",
                Apellidos = "Kennedy",
                Edad = 33,
                Direccion = "Address",
                Telefono = "098575595"
             };
        }

        public static ClientePayloadModel GetMockedClientePayload(string identificacion)
        {
            return new ClientePayloadModel()
            {
                Identificacion = identificacion,
                EstadoCivil = (TipoEstadoCivil)1,
                IdentificacionConyuge = "1234567890",
                NombreConyuge = "Jane Doe",
                SujetoDeCredito = true,
                Nombres = "John F.",
                Apellidos = "Kennedy",
                Edad = 33,
                Direccion = "Address",
                Telefono = "098575595"
            };
        }

        public static Cliente GetMockedClienteDbo(string identificacion)
        {
            return new Cliente()
            {
                Id = 1,
                Identificacion = identificacion,
                EstadoCivil = 1,
                IdentificacionConyuge = "1234567890",
                NombreConyuge = "Jane Doe",
                SujetoDeCredito = true,
                Nombres = "John F.",
                Apellidos = "Kennedy",
                Edad = 33,
                Direccion = "Address",
                Telefono = "098575595"
            };
        }

        public static SolicitudCreditoPayloadModel GetMockedSolicitudCreditoPayload(string idCliente,
            string idEjecutivo = "", string idPatio = "", string idVeh = "")
        {
            return new SolicitudCreditoPayloadModel()
            {
                IdCliente = idCliente,
                FechaElaboracion = DateTime.Now,
                Cuotas = 6,
                Entrada = 100,
                Estado = TipoEstadoSolicitud.Registrada,
                IdEjecutivo = idEjecutivo,
                MesesPlazo = 6,
                Observacion = "Observacion",
                Patio = idPatio,
                PlacaVehiculo = idVeh,
            };
        }

        public static SolicitudCreditoResponseModel GetMockedSolicitudCreditoResponse(string idCliente, string idPatio = "",
            string idVeh = "", string idEjecutivo = "")
        {
            return new SolicitudCreditoResponseModel()
            {
                Id =  1,
                Cliente = new ClienteResponseModel { Id = 1, Identificacion = idCliente },
                FechaElaboracion = DateTime.Now,
                Cuotas = 6,
                Entrada = 100,
                Estado = 1,
                MesesPlazo = 6,
                Observacion = "Observacion",
                Patio = new PatioResponseModel() {Id = 1, Nombre = idPatio },
                Ejecutivo =  new EjecutivoResponseModel() { Id = 1, Identificacion = idEjecutivo },
                Vehiculo = new VehiculoResponseModel { Id =  1, Placa = idVeh}
            };
        }

        public static SolicitudCredito GetMockedSolicitudCreditoDbo(string idCliente, string idPatio = "",
            string idVeh = "", string idEjecutivo = "")
        {
            return new SolicitudCredito()
            {
                Id = 1,
                FechaElaboracion = DateTime.Now,
                Cuotas = 6,
                Entrada = 100,
                Estado = 1,
                MesesPlazo = 6,
                Observacion = "Observacion",
                Ejecutivo = new Ejecutivo { Id = 1, Identificacion = idEjecutivo },
                Vehiculo = new Vehiculo { Id = 1, Placa = idVeh },
                Cliente = new Cliente { Id = 1, Identificacion = idCliente },
                Patio =  new Patio { Id = 1, Nombre = idPatio }
            };
        }
    }
}
