using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Schema.Generator
{
    public class SchemaGenerator
    {
        private readonly getEnumList _enumList;
        public SchemaGenerator(getEnumList enumList)
        {
            _enumList = enumList;
        }
        public async Task<dynamic> Generate<T>(HttpContext httpContext, string contest = null)
        {
            var obj = typeof(T).Name;
            Dictionary<string, dynamic> schema = new Dictionary<string, dynamic>();
            List<dynamic> form = new List<dynamic>();

            System.Attribute[] topattrs = System.Attribute.GetCustomAttributes(typeof(T));
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int x = 0;
            foreach (PropertyInfo prop in Props)
            {

                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    GSchemaAttribute schemaAttr = attr as GSchemaAttribute;
                    if (schemaAttr != null)
                    {
                        string propName = prop.Name;
                        string name = schemaAttr.getName;
                        string title = schemaAttr.getTitle;
                        bool isrequired = schemaAttr.getIsRequired;
                        string regularExpression = schemaAttr.getRegularExpression;
                        string propType = schemaAttr.getType;
                        string defaultValue = schemaAttr.getDefaultValue;
                        string description = schemaAttr.getDiscription;
                        string htmlClass = schemaAttr.getHtmlClass;
                        string fieldHtmlClass = schemaAttr.getfieldHtmlClass;
                        string placeholder = schemaAttr.getPlaceHolder;
                        string activeClass = schemaAttr.getactiveClass;
                        bool message = schemaAttr.getmessage;
                        bool exclusiveMaximum = schemaAttr.getexclusiveMaximum;
                        bool exclusiveMinimum = schemaAttr.getexclusiveMinimum;
                        string EnumVal = schemaAttr.getEnumVal;
                        string minimum = schemaAttr.getminimum;
                        string maximum = schemaAttr.getMaximun;
                        string nameval = string.IsNullOrWhiteSpace(name) ? propName : name;
                        bool multiple = schemaAttr.getMulitple;

                        Dictionary<string, dynamic> schemaObj = new Dictionary<string, dynamic>();
                        dynamic formObj = new ExpandoObject();

                        if(propType == "Array")
                        {
                            schemaObj.Add("type", "array");
                            Dictionary<string, dynamic> innerSchemaObj = new Dictionary<string, dynamic>();
                            innerSchemaObj.Add("type", "object");
                            innerSchemaObj.Add("title", title);
                            var scm = await GenerateInnerSchema(prop, contest);
                            innerSchemaObj.Add("properties", scm.schema);
                            dynamic innerForm = new ExpandoObject();
                            innerForm.key = nameval;
                            innerForm.type = "tabarray";
                            dynamic innerItems = new ExpandoObject();
                            innerItems.type = "section";
                            innerItems.items = scm.form;
                            innerForm.items = innerItems;

                            schema.Add(nameval, schemaObj);
                            form.Add(innerForm);
                        }
                        else
                        {
                            //schema 
                            schemaObj.Add("type", "string");
                            if (!string.IsNullOrWhiteSpace(title) ) schemaObj.Add("title", title);
                            if (!string.IsNullOrWhiteSpace(defaultValue) ) schemaObj.Add("default", defaultValue);
                            if (!string.IsNullOrWhiteSpace(description) ) schemaObj.Add("description", description);
                            if (isrequired && contest == "Edit" ) schemaObj.Add("required", true);
                            if (!string.IsNullOrWhiteSpace(EnumVal) ) schemaObj.Add("enum", await _enumList.getEnumRecords(EnumVal, contest));
                            if (!string.IsNullOrWhiteSpace(regularExpression) ) schemaObj.Add("pattern", regularExpression);
                            if (message ) schemaObj.Add("messages", await _enumList.getVlidationMessage(nameval));
                            if (!string.IsNullOrWhiteSpace(minimum) ) schemaObj.Add("minimum", minimum);
                            if (!string.IsNullOrWhiteSpace(maximum) ) schemaObj.Add("maximum", maximum);
                            if (exclusiveMinimum ) schemaObj.Add("exclusiveMinimum", exclusiveMinimum);
                            if (exclusiveMaximum ) schemaObj.Add("exclusiveMaximum", exclusiveMaximum);

                            schema.Add(nameval, schemaObj);

                            //form Obj
                            formObj.key = nameval;
                            if (!string.IsNullOrEmpty(propType) && !propType.Equals("string") ) formObj.type = propType;
                            if (!string.IsNullOrEmpty(placeholder) ) formObj.placeholder = placeholder;
                            if (!string.IsNullOrEmpty(htmlClass) ) formObj.htmlClass = htmlClass;
                            if (!string.IsNullOrEmpty(fieldHtmlClass) ) formObj.fieldHtmlClass = fieldHtmlClass;
                            if (!string.IsNullOrEmpty(activeClass) ) formObj.activeClass = activeClass;

                            form.Add(formObj);
                        }                    

                    }
                }
                x++;
            }
            dynamic fsub = new ExpandoObject();
            fsub.type = "submit";
            fsub.title = "Submit";
            form.Add(fsub);


            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                schema=schema,
                form=form
            });
        }
        private async Task<dynamic> GenerateInnerSchema(PropertyInfo prop, string contest = "")
        {
            var nam = prop.Name;
            Dictionary<string,dynamic> schema = new Dictionary<string, dynamic>();
            List<dynamic> form = new List<dynamic>();
            var pp = prop.PropertyType.GetGenericArguments()[0].Name;
            PropertyInfo[] Props1 = prop.PropertyType.GetGenericArguments()[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int y = 0;
            foreach (PropertyInfo prop1 in Props1)
            {
                object[] attrs = prop1.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    GSchemaAttribute schemaAttr = attr as GSchemaAttribute;
                    if (schemaAttr != null)
                    {
                        string propName = prop1.Name;
                        string name = schemaAttr.getName;
                        string title = schemaAttr.getTitle;
                        bool isrequired = schemaAttr.getIsRequired;
                        string regularExpression = schemaAttr.getRegularExpression;
                        string propType = schemaAttr.getType;
                        string defaultValue = schemaAttr.getDefaultValue;
                        string description = schemaAttr.getDiscription;
                        string htmlClass = schemaAttr.getHtmlClass;
                        string fieldHtmlClass = schemaAttr.getfieldHtmlClass;
                        string placeholder = schemaAttr.getPlaceHolder;
                        string activeClass = schemaAttr.getactiveClass;
                        bool message = schemaAttr.getmessage;
                        bool exclusiveMaximum = schemaAttr.getexclusiveMaximum;
                        bool exclusiveMinimum = schemaAttr.getexclusiveMinimum;
                        string EnumVal = schemaAttr.getEnumVal;
                        string minimum = schemaAttr.getminimum;
                        string maximum = schemaAttr.getMaximun;
                        string nameval = string.IsNullOrWhiteSpace(name) ? propName : name;
                        Dictionary<string, dynamic> schemaObj = new Dictionary<string, dynamic>();
                        Dictionary<string, dynamic> formObj = new Dictionary<string, dynamic>();

                        //schema 
                        schemaObj.Add("type", "string");
                        if (!string.IsNullOrEmpty(title) && propType != "Array") schemaObj.Add("title", title);
                        if (!string.IsNullOrEmpty(defaultValue) && propType != "Array") schemaObj.Add("default", defaultValue);
                        if (!string.IsNullOrEmpty(description) && propType != "Array") schemaObj.Add("description", description);
                        if (isrequired && propType != "Array") schemaObj.Add("required", isrequired);
                        if (!string.IsNullOrEmpty(EnumVal) && propType != "Array") schemaObj.Add("enum", await _enumList.getEnumRecords(EnumVal, contest));
                        if (!string.IsNullOrEmpty(regularExpression) && propType != "Array") schemaObj.Add("pattern", regularExpression);
                        if (message && propType != "Array") schemaObj.Add("messages", await _enumList.getVlidationMessage(nameval));
                        if (!string.IsNullOrEmpty(minimum) && propType != "Array") schemaObj.Add("minimum", minimum);
                        if (!string.IsNullOrEmpty(maximum) && propType != "Array") schemaObj.Add("maximum", maximum);
                        if (exclusiveMinimum && propType != "Array") schemaObj.Add("exclusiveMinimum", exclusiveMinimum);
                        if (exclusiveMaximum && propType != "Array") schemaObj.Add("exclusiveMaximum", exclusiveMaximum);

                        //Form
                        formObj.Add(nam + "[]." + propName, "");
                        schema.Add(nameval,schemaObj);
                        form.Add(formObj);
                    }
                }
                y++;
            }

            return new 
            {
                form = form,
                schema = schema
            };
        }
    }
}
