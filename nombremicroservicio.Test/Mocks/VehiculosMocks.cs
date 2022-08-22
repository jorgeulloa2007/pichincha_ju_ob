using creditoautomovilistico.API.Models;
using creditoautomovilistico.Infrastructure.Models;

namespace creditoautomovilistico.Test.Mocks
{
    public static class VehiculosMocks
    {
        public static VehiculoResponseModel GetMockedVehiculo(string placa)
        {
            return new VehiculoResponseModel()
            {
                Id = 1,
                Placa = placa,
                Modelo = "1",
                NroChasis = "1",
                Tipo = "Tipo",
                Cilindraje = 1,
                Avaluo = 1,
                MarcaId = 1
            };
        }

        public static VehiculoPayloadModel GetMockedVehiculoPayload(string placa)
        {
            return new VehiculoPayloadModel()
            {
                Placa = placa,
                Modelo = "1",
                NroChasis = "1",
                Tipo = "Tipo",
                Cilindraje = 1,
                Avaluo = 1,
                MarcaId = 1
            };
        }

        public static VehiculoSearchModel GetMockedSearchModel()
        {
            return new VehiculoSearchModel()
            {
                Marca = "1",
                Modelo = "1",
                Year = 2020
            };
        }

        public static Vehiculo GetMockedVehiculoDbo(string placa)
        {
            return new Vehiculo()
            {
                Id = 1,
                Placa = placa,
                Modelo = "1",
                NroChasis = "1",
                Tipo = "Tipo",
                Cilindraje = 1,
                Avaluo = 1,
                MarcaId = 1
            };
        }
    }
}
