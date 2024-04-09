var PersonnelDetails = {
    GetAllPersonnel: function () {
        var url = "http://localhost:5249/api/Personnel/GetAll";
        var ObjPersonnel = "";
        AjaxManager.GETAPI(url, onSuccess, onFailled);

        function onSuccess(Jsondata) {
            ObjPersonnel = Jsondata
        }
        function onFailled(error) {
            alert("Erreur " + error.statusText);
        }
        return ObjPersonnel;
    },
    loadPersonnel: function () {
        var list = this.GetAllPersonnel();
        $.each(list, function (i, personnel) {
            var row = "<tr>" +
                "<td>" + personnel.id + "</td>" +
                "<td>" + personnel.noms + "</td>" +
                "<td>" + personnel.matricule + "</td>" +
                "<td>" + personnel.grade + "</td>" +
                "</tr>";
            $("table tbody").append(row);
        });
    }
}


