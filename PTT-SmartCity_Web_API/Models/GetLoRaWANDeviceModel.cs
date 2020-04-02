using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetLoRaWANDeviceModel
    {
        public GetLoRaWANDevice[] items { get; set; }
        public int totalItems { get; set; }
        public Pager Pager { get; set; }
    }

    public class GetLoRaWANDevice
    {
        public string DevEUI { get; set; }
        public string DevType { get; set; }
        public string GatewayEUI { get; set; }
        public string DevModel { get; set; }
        public DateTime? Updated { get; set; }
    }

    public class LoRaWANDeviceFilterOptions
    {
        [Required]
        public int startPage { get; set; }

        [Required]
        public int limitPage { get; set; }

        public string searchType { get; set; }
        public string searchText { get; set; }
    }
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 5)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 2;
            var endPage = currentPage + 2;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}