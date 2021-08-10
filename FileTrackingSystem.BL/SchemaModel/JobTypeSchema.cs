﻿using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class JobTypeSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public int Id { get; set; } = 0;
        [GSchema("Name", "Name", "string", true, getHtmlClass = "col-md-12")]
        public string Name { get; set; }
        [GSchema("documentRequired", "Document Required", "string", true,getEnumVal ="docReq", getHtmlClass = "col-md-12")]
        public string documentRequired { get; set; }
        [GSchema("status", "Status", "string", true, getEnumVal = "status", getHtmlClass = "col-md-12")]
        public StatusType status { get; set; }
    }
}
