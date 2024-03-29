﻿using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Querys
{
    public class ReservaQuery : IReservaQuery
    {
        private readonly ReservaContext _context;

        public ReservaQuery(ReservaContext context)
        {
            _context = context;
        }

        public Reserva GetReservaById(int reservaId)
        {
            var reserva = _context.Reservas.FirstOrDefault(x => x.ReservaId == reservaId);

            return reserva;
        }

        public List<Reserva> GetReservaList()
        {
            var reservaList = _context.Reservas.ToList();

            return reservaList;
        }

        public List<Reserva> GetReservaListFilters(string fecha, string clase, string orden)
        {
            var reservaList = _context.Reservas
                .OrderBy(p => p.Precio)
                .ToList();

            if (fecha != null)
            {
                DateTime dateTime = DateTime.Parse(fecha);
                reservaList = reservaList.Where(p => p.Fecha == dateTime).ToList();
            }

            if (clase != null)
            {
                reservaList = reservaList.Where(p => p.Clase.ToLower().Contains(clase.ToLower())).ToList();
            }

            if (orden.ToLower() == "desc")
            {
                reservaList = reservaList.OrderByDescending(p => p.Precio).ToList();
            }

            return reservaList;
        }

        public bool ExisteReservaPagada(int reservaId)
        {
            var reserva = _context.Reservas
                .Include(s => s.Pago)
                .ThenInclude(s => s.Factura)
                .FirstOrDefault(x => x.ReservaId == reservaId);

            if (reserva != null)
            {
                if (reserva.Pago?.Factura?.Estado?.ToLower() == "paga")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
