using AdminApplication.Models;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.Controllers
{
    public class OrderController : Controller
    {
        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();


            string URI = "https://localhost:44342/api/Admin/GetOrders";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<Order>>().Result;

            return View(result);
        }
        public IActionResult GetTickets()
        {
            HttpClient client = new HttpClient();


            string URI = "https://localhost:44342/api/Admin/GetTickets";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<Ticket>>().Result;

            return View(result);
        }
        public IActionResult Details(Guid id)
        {
            HttpClient client = new HttpClient();


            string URI = "https://localhost:44342/api/Admin/GetDetailsForTickets";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;


            var result = responseMessage.Content.ReadAsAsync<Order>().Result;

            return View(result);
        }
        [HttpGet]
        public FileContentResult ExportAllTickets(string genre)
        {
            string fileName = "Tickets.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Tickets");

                worksheet.Cell(1, 1).Value = "Movie Name";
                worksheet.Cell(1, 2).Value = "Genre";
                worksheet.Cell(1, 3).Value = "Price";

                HttpClient client = new HttpClient();


                string URI = "https://localhost:44342/api/Admin/GetTickets";

                HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

                var result = responseMessage.Content.ReadAsAsync<List<Ticket>>().Result;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    if (!string.IsNullOrEmpty(genre))
                    {
                        if (item.Genre.Equals(genre))
                        {
                            worksheet.Cell(i + 1, 1).Value = item.MovieName;
                            worksheet.Cell(i + 1, 2).Value = item.Genre;
                            worksheet.Cell(i + 1, 3).Value = item.Price;
                        }
                    }
                    else
                    {
                        worksheet.Cell(i + 1, 1).Value = item.MovieName;
                        worksheet.Cell(i + 1, 2).Value = item.Genre;
                        worksheet.Cell(i + 1, 3).Value = item.Price;
                    }

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }
        public FileContentResult CreateInvoice(Guid id)
        {
            HttpClient client = new HttpClient();


            string URI = "https://localhost:44342/api/Admin/GetDetailsForTickets";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;


            var result = responseMessage.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);


            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());
            document.Content.Replace("{{UserName}}", result.OrderedBy.UserName);

            StringBuilder sb = new StringBuilder();

            float totalPrice = 0;

            foreach (var item in result.TicketsInOrder)
            {
                totalPrice += item.Quantity * item.Ticket.Price;
                sb.AppendLine(item.Ticket.Price + " with quantity of: " + item.Quantity + " and price of: " + item.Ticket.Price + "$");
            }


            document.Content.Replace("{{TicketList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", totalPrice.ToString() + "$");


            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}
