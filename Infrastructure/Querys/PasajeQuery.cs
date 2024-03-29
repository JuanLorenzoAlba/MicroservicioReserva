﻿using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Querys
{
    public class PasajeQuery : IPasajeQuery
    {
        private readonly ReservaContext _context;

        public PasajeQuery(ReservaContext context)
        {
            _context = context;
        }

        public Pasaje GetPasajeById(int pasajeId)
        {
            var pasaje = _context.Pasajes.FirstOrDefault(x => x.PasajeId == pasajeId);

            return pasaje;
        }

        public List<Pasaje> GetPasajeList()
        {
            var pasajeList = _context.Pasajes.ToList();

            return pasajeList;
        }
    }
}
