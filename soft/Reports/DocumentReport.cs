using ged.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ged.Reports
{
    public class DocumentReport
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;

        // variables config reports
        int _totalColumn = 3;
        Document _pdoc;
        BaseColor bgcolor = BaseColor.LIGHT_GRAY;

        Font _fontStyleEntete = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD);
        Font _fontStyleEnteteSmall = new Font(Font.FontFamily.HELVETICA, 5f, Font.BOLD);
        // font style info
        Font _fontStyleInfo = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
        Font _fontStyleInfolIatlic = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLDITALIC);
        Font _fontStyleInfoItalicSmall = new Font(Font.FontFamily.HELVETICA, 5f, Font.BOLDITALIC);

        Font _fontStyleTitre = new Font(Font.FontFamily.HELVETICA, 15f, Font.BOLD);
        Font _fontStyleSousTitreSmall = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
        PdfPTable _tableau = new PdfPTable(3);
        MemoryStream _memoryStream = new MemoryStream();
        public DocumentReport()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        public byte[] prepareReportDocByYear(string annee)
        {
            // iTextSharp.text.Image logoM=default(iTextSharp.text.Image);
            //  logoM = iTextSharp.text.Image.GetInstance("./wwwroot/img/images/logo.png");
            Image logo = Image.GetInstance("./wwwroot/img/images/logo.png");

            _pdoc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _pdoc.SetPageSize(PageSize.A4);
            _pdoc.SetMargins(10f, 10f, 10f, 10f);

            PdfWriter.GetInstance(_pdoc, _memoryStream);
            _pdoc.Open();
            // Paragraph p = new Paragraph("Hello wold : " + membre.Noms + DateTime.Today.Year);
            PdfPTable tableEntete = new PdfPTable(3);
            PdfPCell cell = new PdfPCell(new Phrase("REPUBLIQUE DU CAMEROUN", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell();
            cell.Rowspan = 10;
            //logo
            //Image logo = Image.GetInstance("./wwwroot/img/images/logo.png");
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleAbsolute(70, 50);
            logo.Border = Rectangle.BOX;
            cell.AddElement(logo);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tableEntete.AddCell(cell);
            // end logo
            cell = new PdfPCell(new Phrase("REPUBLIC OF CAMEROUN", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("Paix-Travail-Patrie", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("Peace-Work-Fatherland", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("UNIVERSITE DE YAOUNDE I", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell(new Phrase("THE UNIVERSITY OF YAOUNDE I", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("ECOLE NORMALE SUPERIEURE", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell(new Phrase("HIGHER TEACHERS TRAINING COLLEGE", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("DEPARTEMENT DES ETUDES", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell(new Phrase("DEPARTEMNENT OF STUDIES", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);

            cell = new PdfPCell(new Phrase("SERVICE DE LA SCOLARITE", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            cell = new PdfPCell(new Phrase("SCHOOLING SERVICE", _fontStyleEntete));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tableEntete.AddCell(cell);
            _pdoc.Add(tableEntete);

            Paragraph p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
            _pdoc.Add(p);
            // titre Fiche
            PdfPTable tableTitreFiche = new PdfPTable(1);

            cell = new PdfPCell(new Phrase("LISTES DES DOCUMENTS ", _fontStyleTitre));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.BackgroundColor = bgcolor;
            cell.BackgroundColor = bgcolor;
            tableTitreFiche.AddCell(cell);
            cell = new PdfPCell(new Phrase("LIST OF DOCUMENT ", _fontStyleSousTitreSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.BackgroundColor = bgcolor;
            tableTitreFiche.AddCell(cell);

            cell = new PdfPCell(new Phrase("Pour le compte de l'année :  " + annee, _fontStyleSousTitreSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.BackgroundColor = bgcolor;
            tableTitreFiche.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", _fontStyleEnteteSmall));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.BackgroundColor = bgcolor;
            tableTitreFiche.AddCell(cell);
            _pdoc.Add(tableTitreFiche);

            p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
            _pdoc.Add(p);

            List<Doc> list = new List<Doc>();
            if (annee != null)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/years/" + annee).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Doc>>(data);
                }
                if (list != null)
                {
                    List<Doc> pvs = list.Where(d => d.TypeDoc == "PV").ToList();
                    List<Doc> arretes = list.Where(d => d.TypeDoc == "ARR").ToList();
                    List<Doc> communiques = list.Where(d => d.TypeDoc == "CRP").ToList();
                    List<Doc> autres = list.Where(d => d.TypeDoc == "AUTRE").ToList();
                    if (pvs != null)
                    {
                        // titre PV
                        PdfPTable tableTitreHisto = new PdfPTable(1);

                        cell = new PdfPCell(new Phrase("I.  LES PROCES-VERBAUX", _fontStyleTitre));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        //cell.BackgroundColor = bgcolor;
                        tableTitreHisto.AddCell(cell);
                        _pdoc.Add(tableTitreHisto);

                        // ENTETE TABLEAU PVS
                        float[] colwidthtableHistoAs = { 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfPTable tableHistoAss = new PdfPTable(colwidthtableHistoAs);
                        tableHistoAss.WidthPercentage = 95;
                        cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Source", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Promotion", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Annee de sortie", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Session", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Cycle", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Filière", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Numérisé ?", _fontStyleInfo));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = bgcolor;
                        tableHistoAss.AddCell(cell);
                        //fin entete

                        int cpt = 0;
                        foreach (var pv in pvs)
                        {
                            cpt++;
                            cell = new PdfPCell(new Phrase("" + cpt, _fontStyleInfolIatlic));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.BackgroundColor = bgcolor;
                            tableHistoAss.AddCell(cell);
                            cell = new PdfPCell(new Phrase("" + pv.Source, _fontStyleInfolIatlic));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.BackgroundColor = bgcolor;
                            tableHistoAss.AddCell(cell);
                            cell = new PdfPCell(new Phrase("" + pv.Promotion, _fontStyleInfolIatlic));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.BackgroundColor = bgcolor;
                            tableHistoAss.AddCell(cell);
                            cell = new PdfPCell(new Phrase("" + pv.AnneeSortie, _fontStyleInfolIatlic));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.BackgroundColor = bgcolor;
                            tableHistoAss.AddCell(cell);
                            cell = new PdfPCell(new Phrase("" + pv.Session, _fontStyleInfolIatlic));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.BackgroundColor = bgcolor;
                            tableHistoAss.AddCell(cell);
                            cell = new PdfPCell(new Phrase("" + pv.Cycle.Code, _fontStyleInfolIatlic));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.BackgroundColor = bgcolor;
                            tableHistoAss.AddCell(cell);
                            cell = new PdfPCell(new Phrase("" + pv.Filiere.Code, _fontStyleInfolIatlic));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.BackgroundColor = bgcolor;
                            tableHistoAss.AddCell(cell);
                            if (pv.Fichier == 1)
                            {
                                cell = new PdfPCell(new Phrase("Oui", _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableHistoAss.AddCell(cell);
                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase("Non", _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableHistoAss.AddCell(cell);
                            }


                        }
                        _pdoc.Add(tableHistoAss);
                        float[] colwidthtableArretes = { 0.5f, 1f, 2f, 1f, 1f, 1f, 1f };
                        PdfPTable tableArretes = new PdfPTable(colwidthtableArretes);
                        tableArretes.WidthPercentage = 95;
                        if (arretes != null)
                        {
                            // titre arretes
                            PdfPTable tableTitreArrete = new PdfPTable(1);

                            cell = new PdfPCell(new Phrase("II.  LES ARRETES", _fontStyleTitre));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = Rectangle.NO_BORDER;
                            //cell.BackgroundColor = bgcolor;
                            tableTitreArrete.AddCell(cell);
                            _pdoc.Add(tableTitreArrete);

                            // ENTETE TABLEAU ARRETES
                           
                            cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableArretes.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Source", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableArretes.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Numero", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableArretes.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Date signature", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableArretes.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Année academique", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableArretes.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Cycle", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableArretes.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Numérisé ?", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableArretes.AddCell(cell);
                            //fin entete
                            int i = 0;
                            foreach (var a in arretes)
                            {
                                i++;
                                cell = new PdfPCell(new Phrase("" + i, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableArretes.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + a.Source, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableArretes.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + a.Numero, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableArretes.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + a.DateSign, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableArretes.AddCell(cell);
                                cell = new PdfPCell(new Phrase(" " + a.AnneeAcademique, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableArretes.AddCell(cell);
                                cell = new PdfPCell(new Phrase(" " + a.Cycle.Code, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableArretes.AddCell(cell);
                                if (a.Fichier == 1)
                                {
                                    cell = new PdfPCell(new Phrase("Oui", _fontStyleInfolIatlic));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    //cell.BackgroundColor = bgcolor;
                                    tableArretes.AddCell(cell);
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase("Non", _fontStyleInfolIatlic));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    //cell.BackgroundColor = bgcolor;
                                    tableArretes.AddCell(cell);
                                }
                            }
                            _pdoc.Add(tableArretes);

                        }
                        float[] colwidthtableCRP = { 0.5f, 1f, 2f, 1f, 1f, 1f, 1f, 1f };
                        PdfPTable tableCRP = new PdfPTable(colwidthtableCRP);
                        tableCRP.WidthPercentage = 95;
                        if (communiques != null)
                        {
                            // titre PV
                            PdfPTable tableTitreCommuniques = new PdfPTable(1);

                            cell = new PdfPCell(new Phrase("III.  LES COMMUNIQUES", _fontStyleTitre));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = Rectangle.NO_BORDER;
                            //cell.BackgroundColor = bgcolor;
                            tableTitreCommuniques.AddCell(cell);
                            _pdoc.Add(tableTitreCommuniques);
                            // ENTETE TABLEAU CRP

                            cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Source", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Numero", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Date signature", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Session", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Année academique", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Cycle", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Numérisé ?", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableCRP.AddCell(cell);
                            //fin entete
                            int i = 0;
                            foreach (var c in communiques)
                            {
                                i++;
                                cell = new PdfPCell(new Phrase("" + i, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableCRP.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + c.Source, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableCRP.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + c.Numero, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableCRP.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + c.DateSign, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableCRP.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + c.Session, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableCRP.AddCell(cell);
                                cell = new PdfPCell(new Phrase(" " + c.AnneeAcademique, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableCRP.AddCell(cell);
                                cell = new PdfPCell(new Phrase(" " + c.Cycle.Code, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableCRP.AddCell(cell);
                                if (c.Fichier == 1)
                                {
                                    cell = new PdfPCell(new Phrase("Oui", _fontStyleInfolIatlic));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    //cell.BackgroundColor = bgcolor;
                                    tableCRP.AddCell(cell);
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase("Non", _fontStyleInfolIatlic));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    //cell.BackgroundColor = bgcolor;
                                    tableCRP.AddCell(cell);
                                }
                            }
                            _pdoc.Add(tableCRP);

                        }
                        float[] colwidthtableAutres = { 0.5f, 1f, 2f, 1f, 1f, 1f };
                        PdfPTable tableAutre = new PdfPTable(colwidthtableAutres);
                        tableAutre.WidthPercentage = 95;
                        if (autres != null)
                        {
                            // titre PV
                            PdfPTable tableTitreAutres = new PdfPTable(1);

                            cell = new PdfPCell(new Phrase("IV.  AUTRES", _fontStyleTitre));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = Rectangle.NO_BORDER;
                            //cell.BackgroundColor = bgcolor;
                            tableTitreAutres.AddCell(cell);
                            _pdoc.Add(tableTitreAutres);
                            // ENTETE TABLEAU CRP

                            cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableAutre.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Source", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableAutre.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Numero", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableAutre.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Date signature", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableAutre.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Objet", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableAutre.AddCell(cell);
                            cell = new PdfPCell(new Phrase("Numérisé ?", _fontStyleInfo));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = bgcolor;
                            tableAutre.AddCell(cell);
                            //fin entete
                            int i = 0;
                            foreach (var autr in autres)
                            {
                                i++;
                                cell = new PdfPCell(new Phrase("" + i, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableAutre.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + autr.Source, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableAutre.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + autr.Numero, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableAutre.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + autr.DateSign, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableAutre.AddCell(cell);
                                cell = new PdfPCell(new Phrase("" + autr.Objet, _fontStyleInfolIatlic));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                //cell.BackgroundColor = bgcolor;
                                tableAutre.AddCell(cell);
                                if (autr.Fichier == 1)
                                {
                                    cell = new PdfPCell(new Phrase("Oui", _fontStyleInfolIatlic));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    //cell.BackgroundColor = bgcolor;
                                    tableAutre.AddCell(cell);
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase("Non", _fontStyleInfolIatlic));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    //cell.BackgroundColor = bgcolor;
                                    tableAutre.AddCell(cell);
                                }
                            }
                            _pdoc.Add(tableAutre);
                        }


                    }
                }

                //Membre membre = new Membre();
                //Categorie categorie = new Categorie();
                //List<Paiement> cotisations = new List<Paiement>();
                //List<Assistance> assists = new List<Assistance>();
                //if (id != 0)
                //{
                //    HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
                //    HttpResponseMessage res2 = _httpClient.GetAsync(_httpClient.BaseAddress + "/Paiement/Membre/" + id).Result;
                //    HttpResponseMessage res3 = _httpClient.GetAsync(_httpClient.BaseAddress + "/Demande/Membre/" + id).Result;
                //    if (res.IsSuccessStatusCode)
                //    {
                //        string data = res.Content.ReadAsStringAsync().Result;
                //        membre = JsonConvert.DeserializeObject<Membre>(data);
                //        categorie = membre.Categorie;
                //    }
                //    if (res2.IsSuccessStatusCode)
                //    {
                //        string data2 = res2.Content.ReadAsStringAsync().Result;
                //        cotisations = JsonConvert.DeserializeObject<List<Paiement>>(data2);
                //    }
                //    if (res3.IsSuccessStatusCode)
                //    {
                //        string data3 = res3.Content.ReadAsStringAsync().Result;
                //        assists = JsonConvert.DeserializeObject<List<Assistance>>(data3);
                //    }
                //}

                //// Information sur un membre

                //PdfPTable tablInfo = new PdfPTable(3);
                //// photo profile membre
                //Image photo = Image.GetInstance("./wwwroot/img/photos/" + membre.Matricule + ".png");
                //photo.Alignment = Element.ALIGN_CENTER;
                ////photo.ScaleAbsolute(70, 50);
                //photo.ScaleAbsolute(90, 70);
                //photo.Border = Rectangle.BOX;
                //cell = new PdfPCell();
                //cell.AddElement(photo);
                //cell.Rowspan = 8;

                //cell.Border = Rectangle.NO_BORDER;
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //tablInfo.AddCell(cell);

                //cell = new PdfPCell(new Phrase("    Matricule :", _fontStyleInfolIatlic));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(membre.Matricule, _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Matricule number :", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);

                //cell = new PdfPCell(new Phrase("    Noms & Prenoms :", _fontStyleInfolIatlic));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(membre.Noms + " " + membre.Prenoms, _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Name & Surnames :", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);

                //cell = new PdfPCell(new Phrase("    Fonction :", _fontStyleInfolIatlic));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(membre.Fonction, _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Fonction :", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);

                //cell = new PdfPCell(new Phrase("    Lieu d'affectation :", _fontStyleInfolIatlic));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(membre.Structure.Libele, _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Duty post :", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);

                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Categorie :", _fontStyleInfolIatlic));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(membre.Categorie.libele, _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Category :", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);

                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Telephone :", _fontStyleInfolIatlic));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("" + membre.PhoneNumber, _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase("    Phone number :", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
                //cell.Border = Rectangle.NO_BORDER;
                //tablInfo.AddCell(cell);
                //_pdoc.Add(tablInfo);

                //p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
                //_pdoc.Add(p);

                //// titre Fiche
                //PdfPTable tableTitreHisto = new PdfPTable(1);

                //cell = new PdfPCell(new Phrase("HISTORIQUE COTISATIONS", _fontStyleTitre));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHisto.AddCell(cell);
                //cell = new PdfPCell(new Phrase("TRANSACTIONS HISTORY", _fontStyleSousTitreSmall));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHisto.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Date adhesion : " + membre.DateAdhesion, _fontStyleInfoItalicSmall));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHisto.AddCell(cell);

                //cell = new PdfPCell(new Phrase(" ", _fontStyleEnteteSmall));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHisto.AddCell(cell);
                //_pdoc.Add(tableTitreHisto);
                //p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
                //_pdoc.Add(p);
                //// titre Fiche
                //float[] colwidthtableHisto = { 0.5f, 2f, 2f, 2f };
                //PdfPTable tableHisto = new PdfPTable(colwidthtableHisto);
                //cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHisto.AddCell(cell);

                //cell = new PdfPCell(new Phrase("Periode", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHisto.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Montant", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHisto.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Date cotisation", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHisto.AddCell(cell);
                ////end entete histo cotisation
                //int nbe = 0;
                //foreach (var cotisation in membre.Paiements)
                //{
                //    nbe++;
                //    cell = new PdfPCell(new Phrase("" + nbe, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHisto.AddCell(cell);
                //    cell = new PdfPCell(new Phrase("" + cotisation.PeriodeInfo, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHisto.AddCell(cell);
                //    cell = new PdfPCell(new Phrase("" + cotisation.Montant, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHisto.AddCell(cell);
                //    string date = cotisation.datePaiement.ToString("dd-MM-yyyy");
                //    cell = new PdfPCell(new Phrase("" + date, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHisto.AddCell(cell);
                //}

                //_pdoc.Add(tableHisto);
                //// espace
                //p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
                //_pdoc.Add(p);
                //// ASSISTANCES HISTO
                //// titre Fiche
                //PdfPTable tableTitreHistoAss = new PdfPTable(1);

                //cell = new PdfPCell(new Phrase("HISTORIQUE ASSISTANCES", _fontStyleTitre));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHistoAss.AddCell(cell);
                //cell = new PdfPCell(new Phrase("ASSISTANCE HISTORY", _fontStyleSousTitreSmall));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHistoAss.AddCell(cell);

                //cell = new PdfPCell(new Phrase("Nombre assistances : ", _fontStyleInfoItalicSmall));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHistoAss.AddCell(cell);

                //cell = new PdfPCell(new Phrase(" ", _fontStyleEnteteSmall));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = bgcolor;
                //tableTitreHistoAss.AddCell(cell);
                //_pdoc.Add(tableTitreHistoAss);
                //p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
                //_pdoc.Add(p);
                //// titre Fiche
                //float[] colwidthtableHistoAs = { 0.5f, 2f, 1f, 2f, 2f, 1f };
                //PdfPTable tableHistoAss = new PdfPTable(colwidthtableHistoAs);
                //cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHistoAss.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Objet", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHistoAss.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Date", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHistoAss.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Proposition", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHistoAss.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Montant", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHistoAss.AddCell(cell);
                //cell = new PdfPCell(new Phrase("Statut", _fontStyleInfo));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = bgcolor;
                //tableHistoAss.AddCell(cell);
                ////end entete histo assistance

                //int cpt = 0;
                //foreach (var assistance in assists)
                //{
                //    cpt++;
                //    cell = new PdfPCell(new Phrase("" + cpt, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHistoAss.AddCell(cell);
                //    cell = new PdfPCell(new Phrase("" + assistance.Objet, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHistoAss.AddCell(cell);
                //    string date = assistance.Date.ToString("dd-MM-yyyy");
                //    cell = new PdfPCell(new Phrase("" + date, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHistoAss.AddCell(cell);
                //    cell = new PdfPCell(new Phrase("" + assistance.Proposition, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHistoAss.AddCell(cell);
                //    cell = new PdfPCell(new Phrase("" + assistance.Montant, _fontStyleInfolIatlic));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.BackgroundColor = bgcolor;
                //    tableHistoAss.AddCell(cell);
                //    if (assistance.Statut == 0)
                //    {
                //        cell = new PdfPCell(new Phrase("Enregistré", _fontStyleInfolIatlic));
                //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //        //cell.BackgroundColor = bgcolor;
                //        tableHistoAss.AddCell(cell);
                //    }
                //    else if (assistance.Statut == 1)
                //    {
                //        cell = new PdfPCell(new Phrase("En cours de traitement", _fontStyleInfolIatlic));
                //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //        //cell.BackgroundColor = bgcolor;
                //        tableHistoAss.AddCell(cell);
                //    }
                //    else if (assistance.Statut == 2)
                //    {
                //        cell = new PdfPCell(new Phrase("Validé", _fontStyleInfolIatlic));
                //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //        //cell.BackgroundColor = bgcolor;
                //        tableHistoAss.AddCell(cell);

                //    }
                //    else
                //    {
                //        cell = new PdfPCell(new Phrase("Rejecté", _fontStyleInfolIatlic));
                //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //        //cell.BackgroundColor = bgcolor;
                //        tableHistoAss.AddCell(cell);
                //    }

                //}



                // _pdoc.Add(tableHistoAss);

               

            }
            _pdoc.Close();
            return _memoryStream.ToArray();
        }
    }
}
