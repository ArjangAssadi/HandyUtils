using System.Collections.Generic;
using EventsAggregator.CodeContracts;
using EventsAggregator.StructuralObjects;
using HtmlAgilityPack;
using HtmlDocument = HTMLAgilityPackWrapper.HtmlDocument;
using HtmlNodeCollection = HTMLAgilityPackWrapper.HtmlNodeCollection;

namespace EventsAggregator.CodeObjects.ConversionLogic
{
    public class ConversionLogicsForCanberraEventsWebPage : GenericConversionLogicsForAllWebPage, IConversionLogicsForWebPage
    {
        public int Id
        {
            get { return 1; }
        }

        public TextTable Convert(HtmlDocument document)
        {
            var page = document.GetElementbyId("page");
            HtmlNode namelessDiv = page.ChildNodes[0];

            //the idiots are using one table per event!
            HtmlNodeCollection tables = namelessDiv.SelectNodes("//table") as HtmlNodeCollection;

            TextTable textTable = new TextTable();

            List<TextRow> textRows = new List<TextRow>();
            foreach (HtmlNode table in tables)
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    TextRow textRow = new TextRow();

                    List<string> cells = new List<string>();
                    foreach (HtmlNode cell in row.SelectNodes("th|td"))
                    {
                        cells.Add(cell.InnerHtml);
                    }
                    textRow.Cells = cells;

                    textRows.Add(textRow);
                }
                textTable.Rows = textRows;
            }

            return textTable;
        }


    }
}