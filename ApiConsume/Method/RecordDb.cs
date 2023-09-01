using ApiConsume.Dtos;
using ApiConsume.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ApiConsume.Method
{
    public class RecordDb
    {
        private readonly ApiResumeContext _context;
        public RecordDb(ApiResumeContext conte) { _context = conte; }


        public async Task<bool> GuardarenDb()
        {


            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            string _url = "https://www.bitmex.com/api/v1/announcement";
            var httClient = new HttpClient();
            var response = await httClient.GetAsync(_url);
            var dt = new Dato();
            if (response.IsSuccessStatusCode)
            {


                var contents = await response.Content.ReadAsStringAsync();
                var datos = JsonSerializer.Deserialize<List<Dato>>(contents, options);

                foreach (var e in datos)
                {
                    dt = new Dato { Link = e.Link, Id = e.Id, Content = e.Content, Date = e.Date, Title = e.Title };
                    await _context.Datos.AddAsync(dt);

                }


                await _context.SaveChangesAsync();

            }

            return true;
        }

        public async Task<List<DatosDto>> GetDatos()
        {
            var ListDatos = _context.Datos.ToList();
            var ListDto = new List<DatosDto>();

            if (ListDatos != null) {
                foreach (var dt in ListDatos)
                {
                    ListDto.Add(new DatosDto { Link = dt.Link, Content = dt.Content, Date = dt.Date, Title = dt.Title, Id = dt.Id });
                }
            }
            
            return ListDto;
        }

        public async Task<DatosDto> GetDatosByID(int? id)
        {
            var dtsearched = _context.Datos.FirstOrDefault(d => d.Id == id);
            var dt = new DatosDto();
            if (dtsearched != null)
            {
                dt = new DatosDto { Content = dtsearched.Content, Date = dtsearched.Date, Id = dtsearched.Id, Link = dtsearched.Link, Title = dtsearched.Title };
            }
            

            return dt ;

        }

        public void Actualizar(DatosDto dt)
        {
            var dtupdate = new Dato();
            if (dt != null)
            {
                dtupdate = new Dato { Content = dt.Content, Date = dt.Date, Id = dt.Id, Link = dt.Link, Title = dt.Title };
                _context.Datos.Update(dtupdate);
                _context.SaveChanges();
            }
        }

        public void Borrar(int? id)
        {
            var dtDeleted = _context.Datos.FirstOrDefault(x=> x.Id == id);

            if(dtDeleted != null)
            {
                _context.Datos.Remove(dtDeleted);
            }
            _context.SaveChanges();
            
        }
    }
}
