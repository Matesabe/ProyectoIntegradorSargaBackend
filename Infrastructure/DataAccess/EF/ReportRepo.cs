using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.ReportsInterface;
using Infrastructure.DataAccess.EF.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class ReportRepo : IRepoReport
    {
        SargaContext _context;
        public ReportRepo(SargaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "El contexto no puede ser nulo");
        }

        public int Add(Report obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.type))
                {
                    throw new ArgumentException("El campo 'type' no puede estar vacío", nameof(obj.type));
                }
                obj.id = 0; // Ensure Id is reset for new report
                _context.Reports.Add(obj);
                _context.SaveChanges();
                return obj.id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el reporte: " + ex.Message, ex);
            }
        }

        public IEnumerable<Report> GetAll()
        {
            try
            {
                return _context.Reports
                    .Include(r => r.entries)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los reportes: " + ex.Message, ex);
            }
        }

        public Report GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
