
//    $(document).ready(function(){
//        $('#btnTest').click(function () {
//            $.ajax({
//                //L'URL de la requ�te
//                // url: "https://restcountries.com/v3.1/all",
//                url: "http://localhost:5249/api/Personnels/GetAll",

//                //La m�thode d'envoi (type de requ�te)
//                method: "GET",
//                async: false,
//                cache: false,

//                //Le format de r�ponse attendu
//                dataType: "json",
//                headers: {
//                    'token': 'personnel token',
//                }

//            })
//                //Ce code sera ex�cut� en cas de succ�s - La r�ponse du serveur est pass�e � done()
//                /*On peut par exemple convertir cette r�ponse en chaine JSON et ins�rer
//                 * cette chaine dans un div id="res"*/
//                .done(function (response) {
//                    let data = JSON.stringify(response);
//                    $("div#res").append("-");
//                    $("div#res").append(data);
                  

//                })

//                //Ce code sera ex�cut� en cas d'�chec - L'erreur est pass�e � fail()
//                //On peut afficher les informations relatives � la requ�te et � l'erreur
//                .fail(function (error) {
//                    alert("La requ�te s'est termin�e en �chec. Infos : " + JSON.stringify(error));
//                })

//                //Ce code sera ex�cut� que la requ�te soit un succ�s ou un �chec
//                .always(function () {
//                    alert("Requ�te effectu�e");
//                });
//        });

       
//});
