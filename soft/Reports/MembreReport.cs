//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.collection;
//using Newtonsoft.Json;
//using Org.BouncyCastle.Crypto.Tls;
//using soft.Models;
//using Syncfusion.EJ2.Linq;

//namespace soft.Reports
//{
//    public class MembreReport
//    {
//        Uri baseAdress = new Uri("http://localhost:5249/api");
//        private readonly HttpClient _httpClient;

//        public MembreReport()
//        {
//            _httpClient = new HttpClient();
//            _httpClient.BaseAddress = baseAdress;
//        }

//        int _totalColumn = 3;
//        Document _pdoc;
//        BaseColor bgcolor = BaseColor.LIGHT_GRAY;

//        Font _fontStyleEntete = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD );
//        Font _fontStyleEnteteSmall = new Font(Font.FontFamily.HELVETICA, 5f, Font.BOLD);
//        // font style info
//        Font _fontStyleInfo = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
//        Font _fontStyleInfolIatlic = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLDITALIC);
//        Font _fontStyleInfoItalicSmall = new Font(Font.FontFamily.HELVETICA, 5f, Font.BOLDITALIC);

//        Font _fontStyleTitre = new Font(Font.FontFamily.HELVETICA, 15f, Font.BOLD);
//        Font _fontStyleSousTitreSmall = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
//        PdfPTable _tableau=new PdfPTable(3);
//        MemoryStream _memoryStream=new MemoryStream();
       

//        public byte[] prepareReportCarte(int id=0)
//        {
//            Membre m = new Membre();
//            // iTextSharp.text.Image logoM=default(iTextSharp.text.Image);
//            //  logoM = iTextSharp.text.Image.GetInstance("./wwwroot/img/images/logo.png");
//            Image logo = Image.GetInstance("./wwwroot/img/images/logo.png");
            
//            _pdoc = new Document(new Rectangle(251, 159));
//            _pdoc.SetMargins(0, 0, 0, 0);

//           PdfWriter _writer= PdfWriter.GetInstance(_pdoc, _memoryStream);
//            _pdoc.Open();
           
//            if (id != 0)
//            {
//                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
//                if (res.IsSuccessStatusCode)
//                {
//                    string data = res.Content.ReadAsStringAsync().Result;
//                    m = JsonConvert.DeserializeObject<Membre>(data);
//                }
//            }
//            PdfContentByte canvas1 = _writer.DirectContent;
//            PdfContentByte canvas2 = _writer.DirectContent;
//            PdfContentByte canvasPh = _writer.DirectContent;
//            PdfGState _state1 = new PdfGState();
//            PdfGState _state2=new PdfGState();
//            PdfGState _statePh = new PdfGState();
//            //recto

//            Image recto = Image.GetInstance("./wwwroot/img/images/carte/recto.png");
//            Image verso = Image.GetInstance("./wwwroot/img/images/carte/verso.png");
//            //Image ph = Image.GetInstance("./wwwroot/img/images/logo.png");
           
//            recto.ScaleToFit(265, 179);
//            recto.SetAbsolutePosition(0, 0);
            
//            canvas1.SaveState();
//            canvas1.SetGState( _state1 );
//            canvas1.AddImage(recto);
//            canvas1.RestoreState();
//            string mat = "M233333";
//            // crop circle image
//            //Image ph = Image.GetInstance("./wwwroot/img/photos/M104234.png");
//            Image ph = Image.GetInstance("./wwwroot/img/photos/"+mat+".png");
//            float width = 60f;
//            float height = 60f;
//            PdfContentByte content = _writer.DirectContent;
//            PdfTemplate temp = content.CreateTemplate(width, height);
//            temp.Ellipse(0, 0, width, height);
//            temp.Clip();
//            temp.NewPath();
//            temp.AddImage(ph, width, 0, 0, height, 0, 0);
//            Image clipped = Image.GetInstance(temp);
//            clipped.Border = 2;



//            //canvasPh.SaveState();
//            //canvasPh.SetGState(_statePh);
//            //canvasPh.RoundRectangle(x, y, width, height, raduis);
//            //canvasPh.Clip();
//            //canvasPh.AddImage(ph, 50, 50, 50, 50, 90, 30) ;
//            //canvasPh.Circle(120f, 250f, 50f);
//            //canvasPh.Fill();
//            //canvasPh.RestoreState();
//            clipped.ScaleToFit(60f, 60f);
//            clipped.SetAbsolutePosition(103, 60);
//            canvasPh.SaveState();
//            canvasPh.SetGState(_statePh);
//            //canvasPh.RoundRectangle(x, y, width, height, raduis);
//            //canvasPh.Clip();
//            ////canvasPh.SetCMYKColorFill(0, 0, 0, 0);
//            ////canvasPh.Circle(129, 95, 30);
//            ////canvasPh.Fill();
//            //canvasPh.Circle(120f, 250f, 50f);
//            //canvasPh.Fill();
//            canvasPh.AddImage(clipped);
            
           
//            canvasPh.RestoreState();

//            // verso
//            _pdoc.NewPage();
//           _pdoc.SetMargins(0, 0, 0, 0);
            
//            verso.ScaleToFit(251, 169);
//            verso.SetAbsolutePosition(0, 0);
//            canvas2.SaveState();
//            canvas2.SetGState(_state2);
//            //canvas2.RoundRectangle(x,y, width, height, raduis);
//            canvas2.AddImage(verso);
//            canvas2.RestoreState();
//            _pdoc.Close();
//            return _memoryStream.ToArray();

//        }
//        public byte[] prepareReportFiche(int id = 0)
//        {
//            // iTextSharp.text.Image logoM=default(iTextSharp.text.Image);
//            //  logoM = iTextSharp.text.Image.GetInstance("./wwwroot/img/images/logo.png");
//            Image logo = Image.GetInstance("./wwwroot/img/images/logo.png");

//            _pdoc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
//            _pdoc.SetPageSize(PageSize.A4);
//            _pdoc.SetMargins(10f, 10f, 10f, 10f);

//            PdfWriter.GetInstance(_pdoc, _memoryStream);
//            _pdoc.Open();
//            // Paragraph p = new Paragraph("Hello wold : " + membre.Noms + DateTime.Today.Year);
//            PdfPTable tableEntete = new PdfPTable(3);
//            PdfPCell cell = new PdfPCell(new Phrase("REPUBLIQUE DU CAMEROUN", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell();
//            cell.Rowspan = 10;
//            //logo
//            //Image logo = Image.GetInstance("./wwwroot/img/images/logo.png");
//            logo.Alignment = Element.ALIGN_CENTER;
//            logo.ScaleAbsolute(70, 50);
//            logo.Border = Rectangle.BOX;
//            cell.AddElement(logo);
//            cell.Border = Rectangle.NO_BORDER;
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            tableEntete.AddCell(cell);
//            // end logo
//            cell = new PdfPCell(new Phrase("REPUBLIC OF CAMEROUN", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("Paix-Travail-Patrie", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("Peace-Work-Fatherland", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("MINISTERE DES FORETS ET DE LA FAUNE", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("MINISTRY OF FORESTRY AND WILDLIFE", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("SECRETARIAT D'ETAT", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("SECRETARY OF STATE", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("SECRETARIAT GENERAL", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("SECRETARIAT GENERAL", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("MUPEFOF", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("MUPEFOF", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            _pdoc.Add(tableEntete);

//            Paragraph p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);
//            // titre Fiche
//            PdfPTable tableTitreFiche = new PdfPTable(1);

//            cell = new PdfPCell(new Phrase("FICHE INDIVIDUEL MEMBRE", _fontStyleTitre));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            cell.BackgroundColor = bgcolor;
//            tableTitreFiche.AddCell(cell);
//            cell = new PdfPCell(new Phrase("INDIVIDUAL FORM OF MEMBER", _fontStyleSousTitreSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreFiche.AddCell(cell);

//            cell = new PdfPCell(new Phrase(" ", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreFiche.AddCell(cell);
//            _pdoc.Add(tableTitreFiche);

//            p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);

//            Membre membre = new Membre();
//            Categorie categorie = new Categorie();
//            List<Paiement> cotisations = new List<Paiement>();
//            List<Assistance> assists = new List<Assistance>();
//            if (id != 0)
//            {
//                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
//                HttpResponseMessage res2 = _httpClient.GetAsync(_httpClient.BaseAddress + "/Paiement/Membre/" + id).Result;
//                HttpResponseMessage res3 = _httpClient.GetAsync(_httpClient.BaseAddress + "/Demande/Membre/" + id).Result;
//                if (res.IsSuccessStatusCode)
//                {
//                    string data = res.Content.ReadAsStringAsync().Result;
//                    membre = JsonConvert.DeserializeObject<Membre>(data);
//                    categorie = membre.Categorie;
//                }
//                if (res2.IsSuccessStatusCode)
//                {
//                    string data2 = res2.Content.ReadAsStringAsync().Result;
//                    cotisations = JsonConvert.DeserializeObject<List<Paiement>>(data2);
//                }
//                if (res3.IsSuccessStatusCode)
//                {
//                    string data3 = res3.Content.ReadAsStringAsync().Result;
//                    assists = JsonConvert.DeserializeObject<List<Assistance>>(data3);
//                }
//            }

//            // Information sur un membre
          
//            PdfPTable tablInfo = new PdfPTable(3);
//            // photo profile membre
//            Image photo = Image.GetInstance("./wwwroot/img/photos/" + membre.Matricule + ".png");
//            photo.Alignment = Element.ALIGN_CENTER;
//            //photo.ScaleAbsolute(70, 50);
//            photo.ScaleAbsolute(90, 70);
//            photo.Border = Rectangle.BOX;
//            cell = new PdfPCell();
//            cell.AddElement(photo);
//            cell.Rowspan = 8;

//            cell.Border = Rectangle.NO_BORDER;
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            tablInfo.AddCell(cell);

//            cell = new PdfPCell(new Phrase("    Matricule :", _fontStyleInfolIatlic));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(membre.Matricule, _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_LEFT;
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Matricule number :", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);

//            cell = new PdfPCell(new Phrase("    Noms & Prenoms :", _fontStyleInfolIatlic));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(membre.Noms + " " + membre.Prenoms, _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_LEFT;
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Name & Surnames :", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);

//            cell = new PdfPCell(new Phrase("    Fonction :", _fontStyleInfolIatlic));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(membre.Fonction, _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_LEFT;
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Fonction :", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);

//            cell = new PdfPCell(new Phrase("    Lieu d'affectation :", _fontStyleInfolIatlic));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(membre.Structure.Libele, _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_LEFT;
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Duty post :", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);

//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Categorie :", _fontStyleInfolIatlic));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(membre.Categorie.libele, _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_LEFT;
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Category :", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);

//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Telephone :", _fontStyleInfolIatlic));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("" + membre.PhoneNumber, _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_LEFT;
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase("    Phone number :", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            cell = new PdfPCell(new Phrase(" ", _fontStyleInfoItalicSmall));
//            cell.Border = Rectangle.NO_BORDER;
//            tablInfo.AddCell(cell);
//            _pdoc.Add(tablInfo);

//            p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);

//            // titre Fiche
//            PdfPTable tableTitreHisto = new PdfPTable(1);

//            cell = new PdfPCell(new Phrase("HISTORIQUE COTISATIONS", _fontStyleTitre));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("TRANSACTIONS HISTORY", _fontStyleSousTitreSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Date adhesion : " + membre.DateAdhesion, _fontStyleInfoItalicSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);

//            cell = new PdfPCell(new Phrase(" ", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);
//            _pdoc.Add(tableTitreHisto);
//            p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);
//            // titre Fiche
//            float[] colwidthtableHisto = { 0.5f, 2f, 2f, 2f };
//            PdfPTable tableHisto = new PdfPTable(colwidthtableHisto);
//            cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);

//            cell = new PdfPCell(new Phrase("Periode", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Montant", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Date cotisation", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);
//            //end entete histo cotisation
//            int nbe = 0;
//            foreach (var cotisation in membre.Paiements)
//            {
//                nbe++;
//                cell = new PdfPCell(new Phrase("" + nbe, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + cotisation.PeriodeInfo, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + cotisation.Montant, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//                string date = cotisation.datePaiement.ToString("dd-MM-yyyy");
//                cell = new PdfPCell(new Phrase("" + date, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//            }

//            _pdoc.Add(tableHisto);
//            // espace
//            p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);
//            // ASSISTANCES HISTO
//            // titre Fiche
//            PdfPTable tableTitreHistoAss = new PdfPTable(1);

//            cell = new PdfPCell(new Phrase("HISTORIQUE ASSISTANCES", _fontStyleTitre));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHistoAss.AddCell(cell);
//            cell = new PdfPCell(new Phrase("ASSISTANCE HISTORY", _fontStyleSousTitreSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHistoAss.AddCell(cell);

//            cell = new PdfPCell(new Phrase("Nombre assistances : ", _fontStyleInfoItalicSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHistoAss.AddCell(cell);

//            cell = new PdfPCell(new Phrase(" ", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHistoAss.AddCell(cell);
//            _pdoc.Add(tableTitreHistoAss);
//            p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);
//            // titre Fiche
//            float[] colwidthtableHistoAs = { 0.5f, 2f, 1f, 2f, 2f, 1f };
//            PdfPTable tableHistoAss = new PdfPTable(colwidthtableHistoAs);
//            cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHistoAss.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Objet", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHistoAss.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Date", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHistoAss.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Proposition", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHistoAss.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Montant", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHistoAss.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Statut", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHistoAss.AddCell(cell);
//            //end entete histo assistance

//            int cpt = 0;
//            foreach (var assistance in assists)
//            {
//                cpt++;
//                cell = new PdfPCell(new Phrase("" + cpt, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHistoAss.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + assistance.Objet, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHistoAss.AddCell(cell);
//                string date = assistance.Date.ToString("dd-MM-yyyy");
//                cell = new PdfPCell(new Phrase("" + date, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHistoAss.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + assistance.Proposition, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHistoAss.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + assistance.Montant, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHistoAss.AddCell(cell);
//                if (assistance.Statut == 0)
//                {
//                    cell = new PdfPCell(new Phrase("Enregistré", _fontStyleInfolIatlic));
//                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                    //cell.BackgroundColor = bgcolor;
//                    tableHistoAss.AddCell(cell);
//                }
//                else if (assistance.Statut == 1)
//                {
//                    cell = new PdfPCell(new Phrase("En cours de traitement", _fontStyleInfolIatlic));
//                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                    //cell.BackgroundColor = bgcolor;
//                    tableHistoAss.AddCell(cell);
//                }
//                else if (assistance.Statut == 2)
//                {
//                    cell = new PdfPCell(new Phrase("Validé", _fontStyleInfolIatlic));
//                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                    //cell.BackgroundColor = bgcolor;
//                    tableHistoAss.AddCell(cell);

//                }
//                else
//                {
//                    cell = new PdfPCell(new Phrase("Rejecté", _fontStyleInfolIatlic));
//                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                    //cell.BackgroundColor = bgcolor;
//                    tableHistoAss.AddCell(cell);
//                }

//            }



//            _pdoc.Add(tableHistoAss);

//            _pdoc.Close();
//            return _memoryStream.ToArray();

//        }
//        public byte[] prepareReportMembres()
//        {
//            // iTextSharp.text.Image logoM=default(iTextSharp.text.Image);
//            //  logoM = iTextSharp.text.Image.GetInstance("./wwwroot/img/images/logo.png");
//            Image logo = Image.GetInstance("./wwwroot/img/images/logo.png");

//            _pdoc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
//            _pdoc.SetPageSize(PageSize.A4);
//            _pdoc.SetMargins(10f, 10f, 10f, 10f);

//            PdfWriter.GetInstance(_pdoc, _memoryStream);
//            _pdoc.Open();
//            // Paragraph p = new Paragraph("Hello wold : " + membre.Noms + DateTime.Today.Year);
//            PdfPTable tableEntete = new PdfPTable(3);
//            PdfPCell cell = new PdfPCell(new Phrase("REPUBLIQUE DU CAMEROUN", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell();
//            cell.Rowspan = 10;
//            //logo
//            logo.Alignment = Element.ALIGN_CENTER;
//            logo.ScaleAbsolute(70, 50);
//            logo.Border = Rectangle.BOX;
//            cell.AddElement(logo);
//            cell.Border = Rectangle.NO_BORDER;
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            tableEntete.AddCell(cell);
//            // end logo
//            cell = new PdfPCell(new Phrase("REPUBLIC OF CAMEROUN", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("Paix-Travail-Patrie", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("Peace-Work-Fatherland", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("MINISTERE DES FORETS ET DE LA FAUNE", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("MINISTRY OF FORESTRY AND WILDLIFE", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("SECRETARIAT D'ETAT", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("SECRETARY OF STATE", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("SECRETARIAT GENERAL", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("SECRETARIAT GENERAL", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("-------------", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);

//            cell = new PdfPCell(new Phrase("MUPEFOF", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            cell = new PdfPCell(new Phrase("MUPEFOF", _fontStyleEntete));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            tableEntete.AddCell(cell);
//            _pdoc.Add(tableEntete);

//            Paragraph p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);
           

//            Membre membre = new Membre();
//            List<Membre> list = new List<Membre>();


//            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/").Result;
//            if (res.IsSuccessStatusCode)
//            {
//                string data = res.Content.ReadAsStringAsync().Result;
//                list = JsonConvert.DeserializeObject<List<Membre>>(data);
//            }



//            // titre Fiche
//            PdfPTable tableTitreHisto = new PdfPTable(1);

//            cell = new PdfPCell(new Phrase("LISTES DES MEMBRES", _fontStyleTitre));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("LIST OF MEMBERS", _fontStyleSousTitreSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Date du : " + DateTime.Today.ToString("dd-MM-yyyy"), _fontStyleInfoItalicSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);

//            cell = new PdfPCell(new Phrase(" ", _fontStyleEnteteSmall));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.Border = Rectangle.NO_BORDER;
//            cell.BackgroundColor = bgcolor;
//            tableTitreHisto.AddCell(cell);
//            _pdoc.Add(tableTitreHisto);
//            p = new Paragraph(new Chunk(" ", _fontStyleSousTitreSmall));
//            _pdoc.Add(p);
//            // titre Fiche
//            float[] colwidthtableHisto = { 0.5f, 2f, 1f, 2f};
//            PdfPTable tableHisto = new PdfPTable(colwidthtableHisto);
//            cell = new PdfPCell(new Phrase("N°", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Noms", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Date d'adhesion", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);
//            cell = new PdfPCell(new Phrase("Structure", _fontStyleInfo));
//            cell.HorizontalAlignment = Element.ALIGN_CENTER;
//            cell.BackgroundColor = bgcolor;
//            tableHisto.AddCell(cell);
//            //end entete histo cotisation
//            int i = 0;
//            foreach (var m in list)
//            {
//                i++;
//                cell = new PdfPCell(new Phrase("" + i, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + m.Noms + " "+m.Prenoms , _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + m.DateAdhesion.ToString("dd-MM-yyyy"), _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//                cell = new PdfPCell(new Phrase("" + m.Structure.Code, _fontStyleInfolIatlic));
//                cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                //cell.BackgroundColor = bgcolor;
//                tableHisto.AddCell(cell);
//            }

//            _pdoc.Add(tableHisto);

//            _pdoc.Close();
//            return _memoryStream.ToArray();

//        }

//    }
//}
