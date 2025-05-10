using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Dominio.Entidades;
using Moq;
using Xunit;


namespace ApiFestivos.test
{
    public class FestivosServicioTest
    {

        [Fact]
        public async Task EsFestivo_FechaEsFestivo_True()
        {
            var mockRepo = new Mock<IFestivoRepositorio>();
            mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo>
            {
                new Festivo { Nombre = "Día de la Independencia", Mes = 7, Dia = 20, IdTipo = 1 }
            });

            var servicio = new FestivoServicio(mockRepo.Object);


            var resultado = await servicio.EsFestivo(new DateTime(2024, 7, 20));

            Assert.True(resultado);
        }

        [Fact]
        public async Task EsFestivo_FechaNoEsFestivo_False()
        {
            var mockRepo = new Mock<IFestivoRepositorio>();
            mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo>
            {
                new Festivo { Nombre = "Día de la Independencia", Mes = 7, Dia = 20, IdTipo = 1 }
            });

            var servicio = new FestivoServicio(mockRepo.Object);

            var resultado = await servicio.EsFestivo(new DateTime(2024, 7, 21));

            Assert.False(resultado);
        }

        [Fact]
        public async Task ObtenerFestivo_Tipo1_RetornaFechaCorrecta()
        {
            var mockRepo = new Mock<IFestivoRepositorio>();
            mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo>
            {
                new Festivo { Nombre = "Festivo Fijo", Mes = 1, Dia = 1, IdTipo = 1 }
            });

            var servicio = new FestivoServicio(mockRepo.Object);
            var resultado = await servicio.ObtenerAño(2024);

            Assert.Single(resultado);
            Assert.Equal(new DateTime(2024, 1, 1), resultado.First().Fecha);
        }

        [Fact]
        public async Task ObtenerFestivo_Tipo2_RetornarLunesSiguiente()
        {
           
            var mockRepo = new Mock<IFestivoRepositorio>();
            mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo>
            {
              new Festivo
              {
                  Nombre = "Reyes Magos",
                  Mes = 1,
                  Dia = 6,
                  IdTipo = 2
              }
            });

            var servicio = new FestivoServicio(mockRepo.Object);

            var resultado = await servicio.ObtenerAño(2024);

            var festivo = resultado.FirstOrDefault();
            Assert.NotNull(festivo);
            Assert.Equal(new DateTime(2024, 1, 8), festivo.Fecha);
            Assert.Equal("Reyes Magos", festivo.Nombre);
        }

        [Fact]
        public async Task ObtenerFestivo_Tipo4_SemanaSantaLunesSiguiente()
        {
            var mockRepo = new Mock<IFestivoRepositorio>();
            mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(
            new List<Festivo>
            {
                new Festivo { Nombre = "Festivo Pascua", DiasPascua = 43, IdTipo = 4 }
            });

            var servicio = new FestivoServicio(mockRepo.Object);
            var resultado = await servicio.ObtenerAño(2024);

            Assert.Single(resultado);
            Assert.Equal(new DateTime(2024, 5, 13), resultado.First().Fecha);
        }

    }
}


