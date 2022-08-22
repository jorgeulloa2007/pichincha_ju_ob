using creditoautomovilistico.API.Models;
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

        public static SolicitudCreditoPayloadModel GetMockedSolicitudCreditoPayload(string idCliente)
        {
            return new SolicitudCreditoPayloadModel()
            {
                IdCliente = idCliente,
                FechaElaboracion = System.DateTime.Now,
                Cuotas = 6,
                Entrada = 100,
                Estado = TipoEstadoSolicitud.Registrada,
                IdEjecutivo = "1234567890",
                MesesPlazo = 6,
                Observacion = "Observacion",
                Patio = "Patio",
                PlacaVehiculo = "12345678"
            };
        }

        public static SolicitudCreditoResponseModel GetMockedSolicitudCreditoResponse(string idCliente)
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
                IdPatio = "12345678",
                Ejecutivo =  new EjecutivoResponseModel() { Id = 1 },
                Vehiculo = new VehiculoResponseModel { Id =  1}
            };
        }

        public static SolicitudCredito GetMockedSolicitudCreditoDbo(string idCliente)
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
                Ejecutivo = new Ejecutivo { Id = 1 },
                Vehiculo = new Vehiculo { Id = 1 },
                Cliente = new Cliente { Id = 1, Identificacion = idCliente },
                Patio =  new Patio { Id = 1 }
            };
        }
    }
}
