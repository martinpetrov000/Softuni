﻿namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketCustomerImportDto
    {
        [Required]
        [Range(typeof(decimal), "0.01", "100000000000000")]
        [XmlElement("Price")]
        public decimal Price { get; set; }

        [Required]
        [XmlElement("ProjectionId")]
        public int ProjectionId { get; set; }
    }
}
