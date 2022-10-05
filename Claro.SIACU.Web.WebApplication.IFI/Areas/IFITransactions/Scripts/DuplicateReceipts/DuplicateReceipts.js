var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};
function CloseValidation(obj, pag, controls) {

    if (obj.hidAccion === 'G') {
        var sUser = obj.hidUserValidator;
        FC_GrabarCommit(pag, controls, obj.NamesUserValidator, obj.EmailUserValidator);
    }

    var mensaje;

    if (obj.hidAccion == 'F') {

        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';

        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
};

function FC_GrabarCommit(pag, controls, NamesUserValidator, EmailUserValidator) {
    document.getElementById('hidAccion').value = 'OK';
 

};

function isNumberChar(InString) {
    if (InString.length != 1)
        return (false);
    RefString = "1234567890";
    if (RefString.indexOf(InString, 0) == -1)
        return (false);
    return (true);
};

function isNumber(pString) {
    var ok = "yes"; var temp;
    var valid = "0123456789";
    for (var i = 0; i < pString.length ; i++) {
        temp = "" + pString.substring(i, i + 1);
        if (valid.indexOf(temp) == "-1") ok = "no";
    }
    if (ok == "no") { return (false); } else { return (true); }

}

(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTDuplicateReceipts.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , hidSupJef: $("#hidSupJef", $element)
            , cboCACDAC: $("#cboCACDAC", $element)
            , dgFechas: $("#dgFechas", $element)
            

            //textbox
            , txtCalleDuplicateR: $("#txtCalleDuplicateR", $element)
            , txtReferenciaDuplicateR: $('#txtReferenciaDuplicateR', $element)
            , txtDepartamentoDuplicateR: $('#txtDepartamentoDuplicateR', $element)
            , txtProvinciaDuplicateR: $('#txtProvinciaDuplicateR', $element)
            , txtDistritoDuplicateR: $('#txtDistritoDuplicateR', $element)
            , txtFechaDistribucion: $('#txtFechaDistribucion', $element)
            , txtMedio: $('#txtMedio', $element)
            , cboMedio: $('#cboMedio', $element)
           
            , chkReciboFedateado: $('#chkReciboFedateado', $element)
            , txtEmail: $("#txtEmail", $element)
            , txtNote: $('#txtNote', $element)

            //label : Datos cliente

            , lblContrato: $("#lblContrato", $element)
            , lblTipoCliente: $("#lblTipoCliente", $element)
            , lblContacto: $("#lblContacto", $element)
            , lblCodUbigeo: $("#lblCodUbigeo", $element)
            , lblCliente: $("#lblCliente", $element)
            , lblPlan: $("#lblPlan", $element)
            , lblFechaActivacion: $("#lblFechaActivacion", $element)
            , lblEstLinea: $("#lblEstLinea", $element)
            , lblTipAcu: $("#lblTipAcu", $element)
            , lblActivo: $("#lblActivo", $element)
            , lblDireccion: $("#lblDireccion", $element)
            , lblNotasDirec: $("#lblNotasDirec", $element)
            , lblDepartamento: $("#lblDepartamento", $element)
            , lblDistrito: $("#lblDistrito", $element)
            , lblCodPlano: $("#lblCodPlano", $element)
            , lblPais: $("#lblPais", $element)
            , lblProvincia: $("#lblProvincia", $element)
            , lblTipAcuerdo: $("#lblTipAcuerdo", $element)
            , lblErrorMessage: $("#lblErrorMessage", $element)
            , lblTitle: $('#lblTitle', $element)
            , lblCustomerID: $('#lblCustomerID', $element)
            , lblDNI_RUC: $('#lblDNI_RUC', $element)
            , lblRepren_Legal: $('#lblRepren_Legal', $element)
            , lblCicloFact: $('#lblCicloFact', $element)
            , lblLimiteCred: $('#lblLimiteCred', $element)
            , lblNroTelefono: $('#lblNroTelefono', $element)
            //radiobutton
            , chkEmail: $("#chkEmail", $element)


            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnConstancia: $("#btnConstancia", $element)
            , myModalLoad: $("#myModalLoad", $element)
            , divReglas: $("#divReglas", $element)
            , divErrorAlert: $('#divErrorAlert', $element)


        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();

            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_click);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_Change);
            controls.chkReciboFedateado.addEvent(that, 'change', that.chkReciboFedateado_Change);
            controls.cboMedio.addEvent(that, 'change', that.cboMedio_Change);
          

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },

       

        modelDuplicateReceipts: {},
        render: function () {

            var that = this,
            control = that.getControls();
            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);
            control.divErrorAlert.hide();
            that.Loading();
            that.loadCustomerData();          
            that.InitMedios();
            that.loadTypification();



            control.chkEmail.attr("checked", true);
            if (control.chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }

                   
            that.getRulesControls();
            $("#btnGuardar").attr('disabled', true);

            $("input,select").keydown(function (e) {

                $("#btnGuardar").attr('disabled', false);
            });

            $("select").change(function (e) {
                $("#btnGuardar").attr('disabled', false);
            });

            $("textarea").keydown(function (e) {
                $("#btnGuardar").attr('disabled', false);
            });

            $("input:checkbox").click(function (e) {
                $("#btnGuardar").attr('disabled', false);
            });


        },
        getRulesControls: function () {
          
            //regla personalizada
           
            $.validator.addMethod(
                "ValidateMed",
                function (value, element) {

                    return (value == "" || value == "Seleccionar") ? false : true;
                },
                "* Debe Seleccionar un medio"
                 );

            //reglas declaradas
            $('#formDuplicateReceipts').validate({ // initialize the plugin
                rules: {

                                      
                    nmcboMedio: {
                        ValidateMed: true,
                        required: true
                    },

                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {

                  
                    nmcboMedio: {
                        required: "* Debe seleccionar un medio.",
                    }

                }

            });

        },
        loadTypification: function () {

            var obj = { strIdSession: Session.IDSESSION };
            $.app.ajax({
                type: 'POST',
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/IFITransactions/DuplicateReceipts/PageLoad',
                success: function (response) {

                   

                    if (response.data != null) {

                        TYPIFICATION.ClaseId = response.data.CLASE_CODE;
                        TYPIFICATION.SubClaseId = response.data.SUBCLASE_CODE;
                        TYPIFICATION.Tipo = response.data.TIPO;
                        TYPIFICATION.ClaseDes = response.data.CLASE;
                        TYPIFICATION.SubClaseDes = response.data.SUBCLASE;
                        TYPIFICATION.TipoId = response.data.TIPO_CODE;
                    }

                }

            });
        },

       
        windowAutoSize: function () {
            var hsize = Math.max(
                    document.documentElement.clientHeight,
                    document.body.scrollHeight,
                    document.documentElement.scrollHeight,
                    document.body.offsetHeight,
                    document.documentElement.offsetHeight
                );
            hsize = hsize - 72;
            $('#content').css({ 'height': hsize + 'px' });
        },

        IniBegin: function () {
            var that = this,
            controls = this.getControls();

            that.IniLoadPage();
        },


        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();
            controls.lblTitle.text("Duplicado de Recibos");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
            Session.ACCESO = SessionTransac.SessionParams.USERACCESS;

            Session.DATACUSTOMER =
            {
                Email: Session.CLIENTE.Email,
                Departament_Fact: Session.CLIENTE.InvoiceDepartament,
                Provincia_Fac: Session.CLIENTE.InvoiceProvince,
                Distrito_Fac: Session.CLIENTE.InvoiceDistrict
  
            }

            var obj = {
                strIdSession: Session.IDSESSION,
                strTelefono: Session.CLIENTE.Telephone,
                strCustomerId: Session.CLIENTE.CustomerID
            };
            $.app.ajax({
                type: 'POST',
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/IFITransactions/DuplicateReceipts/GetCustomerDuplicateReceipts',
                success: function (response) {

                    var ObjClientDataAdd = response.objclientDataAdd;
                    var ObjCus = response.objCus;

                    that.setSesssionsCustomer(response.objCus, ObjClientDataAdd);

                },
                complete: function () {

                    //********** Datos del Formulario ***********/
                    controls.txtCalleDuplicateR.val((Session.CLIENTE.Address == null) ? '' : Session.CLIENTE.Address);
                    controls.txtReferenciaDuplicateR.val((Session.CLIENTE.InvoiceUrbanization == null) ? '' : Session.CLIENTE.InvoiceUrbanization);
                    controls.txtDepartamentoDuplicateR.val((Session.DATACUSTOMER.Departament_Fact == null) ? '' : Session.DATACUSTOMER.Departament_Fact);
                    controls.txtProvinciaDuplicateR.val((Session.DATACUSTOMER.Provincia_Fac == null) ? '' : Session.DATACUSTOMER.Provincia_Fac);
                    controls.txtDistritoDuplicateR.val((Session.DATACUSTOMER.Distrito_Fac == null) ? '' : Session.DATACUSTOMER.Distrito_Fac);
                    controls.txtMedio.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);
                    that.LoadDataCheckdgFechas();
                    controls.lblNroTelefono.html((Session.CLIENTE.Telephone == null) ? '' : Session.CLIENTE.Telephone);
                    controls.txtEmail.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);
                    that.InitCacDaC();
                   
                }

            });

            //********** Datos del Cliente ***********/
            controls.lblContrato.html((Session.CLIENTE.ContractID == null) ? '' : Session.CLIENTE.ContractID);
            controls.lblCustomerID.html((Session.CLIENTE.CustomerID == null) ? '' : Session.CLIENTE.CustomerID);
            controls.lblTipoCliente.html((Session.CLIENTE.CustomerType == null) ? '' : Session.CLIENTE.CustomerType);
            controls.lblCliente.html((Session.CLIENTE.BusinessName == null) ? '' : Session.CLIENTE.BusinessName);
            controls.lblContacto.html((Session.CLIENTE.FullName == null) ? '' : Session.CLIENTE.FullName);
            controls.lblDNI_RUC.html((Session.CLIENTE.DNIRUC == null) ? '' : Session.CLIENTE.DNIRUC);
            controls.lblRepren_Legal.html((Session.CLIENTE.LegalAgent == null) ? '' : Session.CLIENTE.LegalAgent);
            controls.lblPlan.html((Session.LINEA.Plan == null) ? '' : Session.LINEA.Plan);
            controls.lblFechaActivacion.html((Session.LINEA.ActivationDate == null) ? '' : Session.LINEA.ActivationDate);
            controls.lblCicloFact.html((Session.CLIENTE.objPostDataAccount.BillingCycle == null) ? '' : Session.CLIENTE.objPostDataAccount.BillingCycle);
            
            controls.lblLimiteCred.html((Session.CLIENTE.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + Session.CLIENTE.objPostDataAccount.CreditLimit);
            //********** Direccíón de Facturación ***********/
            controls.lblDireccion.html((Session.CLIENTE.InvoiceAddress == null) ? '' : Session.CLIENTE.InvoiceAddress);
            controls.lblNotasDirec.html((Session.CLIENTE.Reference == null) ? '' : Session.CLIENTE.Reference);
            controls.lblPais.html((Session.CLIENTE.Country == null) ? '' : Session.CLIENTE.Country);
            controls.lblDepartamento.html((Session.CLIENTE.Departament == null) ? '' : Session.CLIENTE.Departament);
            controls.lblProvincia.html((Session.CLIENTE.Province == null) ? '' : Session.CLIENTE.Province);
            controls.lblDistrito.html((Session.CLIENTE.District == null) ? '' : Session.CLIENTE.District);
            controls.txtCalleDuplicateR.val((Session.CLIENTE.Address == null) ? '' : Session.CLIENTE.Address);
            controls.txtReferenciaDuplicateR.val((Session.CLIENTE.Reference == null) ? '' : Session.CLIENTE.Reference);
            controls.txtDepartamentoDuplicateR.val((Session.CLIENTE.Departament == null) ? '' : Session.CLIENTE.Departament);
            controls.txtProvinciaDuplicateR.val((Session.CLIENTE.Province == null) ? '' : Session.CLIENTE.Province);
            controls.txtDistritoDuplicateR.val((Session.CLIENTE.District == null) ? '' : Session.CLIENTE.District);
            controls.txtMedio.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
            controls.txtEmail.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
            
            that.LoadDataCheckdgFechas();
            
 
        },
        resp: "",
        LoadPag: function () {
            var that = this, controls = this.getControls();
            $.blockUI({
                message: controls.myModalLoad,
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000000',
                    '-webkit-border-radius': '50px',
                    '-moz-border-radius': '50px',
                    opacity: .7,
                    color: '#fff'
                }
            });

        },
        chkReciboFedateado_Change: function () {
            var that = this,
               control = that.getControls(),
               ctrReciboFedateado = control.chkReciboFedateado;

            if (ctrReciboFedateado[0].checked == true) {

                control.chkEmail.prop('checked', false);
                control.chkEmail.prop("disabled", true);
                control.txtEmail.css("display", "none");
            } else {
                control.chkEmail.prop("disabled", false);

            }
        },
        chkEmail_Change: function (sender, arg) {
            var that = this,
                control = that.getControls(),
                chkEmail = control.chkEmail;

            if (chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
        },




        maximizarWindow: function () {
            top.window.moveTo(0, 0);
            if (document.all) {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            } else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;

                }
            }
        },



        btnConstancia_click: function () {
            var that = this;


            var PDFRoute = that.RutaArchivo;
            var IdSession = Session.IDSESSION;
            if (PDFRoute != "") {
                ReadRecordSharedFile(IdSession, PDFRoute);
            }
        },

        btnGuardar_click: function () {

            if ($('#formDuplicateReceipts').valid()) {
            var that = this,
                controls = this.getControls();

            that.isPostBackFlag = that.NumeracionUNO;
               
            if (!that.ValidateControl()) {
                return false;
            }

                controls.chkEmail.attr("checked", true);
            if (controls.chkEmail[0].checked == true) {
                $('#chkEmail').val("1");
            } else {
                $('#chkEmail').val("0");
            }


            confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {

                that.SaveTransactionDuplicateReceipts();

            }, function () {
                $("#hidAccion").val("");
                return false;
            });
            }

        },

      

        ValidateControl: function () {
            var that = this,
                control = that.getControls();


            var arrDates = that.getDateVencimiento();
            var xcadena = "";
            $.each(arrDates, function (index, value) {
                xcadena += value + "|";
            });
            
            if (xcadena == "" || xcadena == null) {
                alert("Debe Seleccionar una fecha", '', function () {                  
                }); return false;
            }

                       
            if ($("#chkEmail").prop("checked")) {
                if ($("#txtEmail").val() == "") {
                    alert("Ingresar email", 'Alerta', function () {
                        control.txtEmail.focus();
                    }); return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtEmail").val());
                if (!blvalidar) {
                    alert("Ingresar email válido", 'Alerta', function () {
                        control.txtEmail.select();
                    }); return false;
                }
            }

            if ($("#cboMedio").val() == 1) {
                
                if ($("#txtMedio").val() == "") {
                    alert("Ingresar correo electronico del medio de entrega", 'Alerta', function () {
                        control.txtMedio.focus();
                    }); return false;
                }

                var regx2 = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar2 = regx2.test($("#txtMedio").val());
                if (!blvalidar2) {
                    alert("Ingresar email válido", 'Alerta', function () {
                        control.txtMedio.select();
                    }); return false;
                }
            }

           

            if (control.cboCACDAC.val() == "-1" || control.cboCACDAC.val() == "") {
                alert("Seleccione Punto de Atención.", "Alerta");
                return false;
            }

         
            var strNotas = control.txtNote.val();

            if (strNotas.length > 3800) {
                alert("El maximo de caracteres permitidos en el campo notas es de 3800");
                control.txtNote.val(strNotas.substring(0, 3800));
                control.txtNote.focus();
                return false;
            }


            return true;
        },


        btnCerrar_Click: function () {
            parent.window.close();
        },


        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },



        BlockControl: function () {
            var that = this,
            control = that.getControls();
                    
            control.txtNote.prop('disabled', true);          
            control.txtMedio.prop('disabled', true);
            control.cboMedio.prop('disabled', true);
            control.txtEmail.prop('disabled', true);
            control.cboCACDAC.prop('disabled', true);
            control.chkEmail.prop('disabled', true);
            $('.clsDates').prop('disabled', true);
            $('.Duplicate').prop('disabled', true);
     
        },

        Loading: function () {
            var that = this;
            var controls = that.getControls();


            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
        },
        SaveTransactionDuplicateReceipts: function () {

            //Variables para tipificacion

            var that = this,
                controls = that.getControls();

            that.modelDuplicateReceipts.strIdSession = Session.IDSESSION;            
            that.modelDuplicateReceipts.strCalle = $('#txtCalleDuplicateR').val();
            that.modelDuplicateReceipts.strReferencia = $('#txtReferenciaDuplicateR').val();
            that.modelDuplicateReceipts.strDepartamento = $('#txtDepartamentoDuplicateR').val();
            that.modelDuplicateReceipts.strProvincia = $('#txtProvinciaDuplicateR').val();
            that.modelDuplicateReceipts.strEmail = $('#txtEmail').val();
            that.modelDuplicateReceipts.strDistrito = $('#txtDistritoDuplicateR').val();
            that.modelDuplicateReceipts.strFechaDistribucion = $('#txtFechaDistribucion').val();            
            that.modelDuplicateReceipts.strCacDac = $('#cboCACDAC option:selected').text();
            that.modelDuplicateReceipts.strCacDacId = $('#cboCACDAC option:selected').val();
            that.modelDuplicateReceipts.strNote = $("#txtNote").val();
            that.modelDuplicateReceipts.strchkEmail = controls.chkEmail.is(':checked');
            that.modelDuplicateReceipts.strchkReciboFedateado = $('#chkReciboFedateado').val();
            that.modelDuplicateReceipts.strMedio = $('#cboMedio option:selected').val();
            that.modelDuplicateReceipts.strMedioDes = $('#cboMedio option:selected').text();
            if ($('#txtMedio').val() == "") {
                that.modelDuplicateReceipts.strEmailMedio = Session.CLIENTE.Email;
            }
            else {
                that.modelDuplicateReceipts.strEmailMedio = $('#txtMedio').val();
            }
           
            that.modelDuplicateReceipts.strFechActivacion = Session.LINEA.ActivationDate;
            that.modelDuplicateReceipts.strCustomerId = Session.CLIENTE.CustomerID;
            that.modelDuplicateReceipts.strObjidContacto = Session.CLIENTE.IdContactObject;
            that.modelDuplicateReceipts.strNombres = Session.CLIENTE.Name;
            that.modelDuplicateReceipts.strApellidos = Session.CLIENTE.LastName;
            that.modelDuplicateReceipts.strNombresCompleto = Session.CLIENTE.FullName;
            that.modelDuplicateReceipts.strTelefono = Session.CLIENTE.Telephone;
            that.modelDuplicateReceipts.DNI_RUC = Session.CLIENTE.DNIRUC;
            that.modelDuplicateReceipts.strRepresentante = Session.CLIENTE.LegalAgent;
            that.modelDuplicateReceipts.strDocRepresentante = Session.CLIENTE.DocumentNumber;
            that.modelDuplicateReceipts.strContactoCliente = Session.CLIENTE.CustomerContact;
            that.modelDuplicateReceipts.strTipoocumento = Session.CLIENTE.DocumentType;
            that.modelDuplicateReceipts.AdressDespatch = Session.CLIENTE.Address;
            that.modelDuplicateReceipts.fullNameUser = Session.ACCESO.fullName;
            that.modelDuplicateReceipts.strPais = Session.CLIENTE.Country;
            that.modelDuplicateReceipts.Accion = "R";
            //datos adicionales
           that.modelDuplicateReceipts.account = Session.CLIENTE.Account;
            that.modelDuplicateReceipts.contractId = Session.CLIENTE.ContractID;
            that.modelDuplicateReceipts.currentUser = Session.ACCESO.login;
            that.modelDuplicateReceipts.customerId = Session.CLIENTE.CustomerID;
            that.modelDuplicateReceipts.tipo = TYPIFICATION.Tipo;
            that.modelDuplicateReceipts.claseDes = TYPIFICATION.ClaseDes;
            that.modelDuplicateReceipts.subClaseDes = TYPIFICATION.SubClaseDes;
            that.modelDuplicateReceipts.claseCode = TYPIFICATION.ClaseId;
            that.modelDuplicateReceipts.subClaseCode = TYPIFICATION.SubClaseId;
            that.modelDuplicateReceipts.tipoCode = TYPIFICATION.TipoId
            if ($('#chkEmail')[0].checked) {
                that.modelDuplicateReceipts.ischkEmail = true;
            } else {
                that.modelDuplicateReceipts.ischkEmail = false;
            }

            //fin--

            var arrDates = that.getDateVencimiento();
            var xcadena = "";
            $.each(arrDates, function (index, value) {
                xcadena += value + "|";
            });
            that.modelDuplicateReceipts.arrDates = xcadena;

            var arrPaths = that.getSelected();
            var cadena = "";
            $.each(arrPaths, function (index, value) {
                cadena += value + "|";
            });

            that.modelDuplicateReceipts.arrPaths = cadena;
            that.modelDuplicateReceipts.Destinatarios = $('#txtEmail').val();

            
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelDuplicateReceipts),
                url: '/IFITransactions/DuplicateReceipts/SaveTransactionDuplicateReceipts',
                error: function (data) {
                    alert(that.strMensajeDeError, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);
                },
                complete: function () {
                    $.unblockUI();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                        baseZ: $.app.getMaxZIndex() + 1,
                        css: {
                            border: 'none',
                            padding: '15px',
                            backgroundColor: '#000',
                            '-webkit-border-radius': '10px',
                            '-moz-border-radius': '10px',
                            opacity: .5,
                            color: '#fff'
                        }
                    });
                },
                success: function (response) {
                    that.MensajeEmail = response.MensajeEmail;
                    that.vDesInteraction = response.vDesInteraction;
                    that.InteractionId = response.vInteractionId;
                    that.RutaArchivo = response.strRutaArchivo;
                    that.hidCasoId = response.hidCasoId;



                    if (that.InteractionId != '' || that.hidCasoId!= '') {
                        $("#btnConstancia").attr('disabled', false); //Activa                        
                        alert(that.vDesInteraction, "Informativo");  //that.vDesInteraction

                        that.BlockControl();
                        $("#btnGuardar").attr('disabled', true);
                    }
                    else if (that.InteractionId == "" || that.hidCasoId == "") {
                        $("#btnConstancia").attr('disabled', true);
                        $("#btnGuardar").attr('disabled', true);
                        alert(that.vDesInteraction, "Informativo");
                    }

                }
            });
        },


        setSesssionsCustomer: function (objCus, objclientDataAdd) {

            Session.CLIENTE.Position = objCus.CARGO;
            Session.CLIENTE.Tradename = objCus.NOMBRE_COMERCIAL;
            Session.CLIENTE.PhoneReference = objCus.TELEF_REFERENCIA;
            Session.CLIENTE.CustomerContact = objCus.CONTACTO_CLIENTE;
            Session.CLIENTE.PhoneContact = objCus.TELEFONO_CONTACTO;
            Session.CLIENTE.Fax = objCus.FAX;
          
            Session.CLIENTE.Email = objCus.EMAIL;
            Session.CLIENTE.Sex = objCus.SEXO;
            Session.CLIENTE.ContactNumberReference1 = objclientDataAdd.PHONE1;
            Session.CLIENTE.ContactNumberReference2 = objclientDataAdd.PHONE2;
            Session.CLIENTE.Urbanization = objclientDataAdd.ADDRESS;
            Session.CLIENTE.PlaneCodeBilling = objclientDataAdd.EMAIL1;
            Session.CLIENTE.PlaneCodeInstallation = objclientDataAdd.EMAIL2;
            Session.CLIENTE.CivilStatusID = objCus.ESTADO_CIVIL_ID;
            Session.CLIENTE.BirthPlaceID = objCus.LUGAR_NACIMIENTO_ID;
            Session.CLIENTE.Departament = objCus.DEPARTAMENTO;
            Session.CLIENTE.Province = objCus.PROVINCIA;
            Session.CLIENTE.District = objCus.DISTRITO;
           
            Session.CLIENTE.BillingCycle = objCus.CICLO_FACTURACION;
           
            Session.CLIENTE.InvoiceAddress = objCus.CALLE_FAC;
            Session.CLIENTE.Reference = objCus.REFERENCIA;
        },

        InitMedios: function () {
            var that = this,
            controls = that.getControls(),
            objCacDacType = {},
            parameters = {};
           
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/IFITransactions/CommonServices/GetDeliveryMedium',
                success: function (response) {
                    controls.cboMedio.append($('<option>', { value: '', html: 'Seleccionar' }));
                                     
                    var i = 0;
                    $.each(response.data, function (index, value) {
                        
                        controls.cboMedio.append($('<option>', { value: response.data[i].Codigo, html: response.data[i].Descripcion }));

                            i++;
                    });                 
 
                }
            });
        },

        cboMedio_Change: function () {

            var objTipoMedio = document.getElementById('cboMedio');
            var objTextoMedio = document.getElementById('txtMedio');

            if (objTipoMedio.value == 1) {
 
                if (Session.CLIENTE.DNIRUC.length == 8) {
                    
                    objTextoMedio.disabled = false;
                    objTextoMedio.style.visibility = 'visible';
                    objTextoMedio.value == Session.CLIENTE.Email;
                }
                else if (Session.CLIENTE.DNIRUC.length == 11) {

                        
                        objTextoMedio.disabled = true;
                        objTextoMedio.style.visibility = 'visible';
                        objTextoMedio.value == Session.CLIENTE.Email;
                }   

            } else {
               
                objTextoMedio.value == Session.CLIENTE.Email;
                objTextoMedio.disabled = true;
                objTextoMedio.style.visibility = 'hidden';
                

            }
        },

        InitCacDaC: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {},
                parameters = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var cacdac = results.Cac;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        cache: false,
                        data: JSON.stringify(objCacDacType),
                        url: '/IFITransactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },
       
        LoadDataCheckdgFechas: function () {
            var that = this;
            var param = {
                strIdSession: Session.IDSESSION,
                strCoid: Session.CLIENTE.ContractID,
                strCustomerId: Session.CLIENTE.CustomerID,
                currentUser: Session.ACCESO.login,
                strFullName: Session.ACCESO.fullName
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/IFITransactions/CommonServices/GetdgFechasType',
                success: function (response) {
                  
                    that.InitDataTable(response.data.LastInvoiceDatas);
                   
                }
            });
        },

        InitDataTable: function (rowsData) {

            var that = this, controls = that.getControls();

            that.dtFechas = $('#dgFechas').DataTable({
                scrollCollapse: true,
                paging: false,
                destroy: true,
                searching: false,
                select: "single",
                info: false,
                data: rowsData,
                paging: false,
                bSort: false,
                columns: [
                   { "data": null },
                   { "data": "FECHA_VENCIMIENTO", type: 'date-dd-mmm-yyyy' }

                ],
                columnDefs: [{


                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" class="clsDates" name="' + full.INVOICENUMBER + ';' + full.FECHA_VENCIMIENTO + ';' + full.PERIODO + '"value="' + $('<div/>').text(data.FECHA_VENCIMIENTO).html() + '">';
                    }

                },
                {
                    'targets': 1,


                    'render': function (data, type, full, meta) {
                        return "<a class='Duplicate' style='cursor: pointer'>" + data + "</a>";
                    }
                },


                ],

                select: {
                    style: 'os',
                    selector: 'td:first-child'
                },
                "initComplete": function (settings, json) {
                    that.toggleCheckbox();
                },
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });
            $(".sorting_dsc").hide();
            $(".sorting_asc").hide();

            $(".sorting_1").css("padding-left", "5px");

            that.duplicateInvoice_click();

            $("#dgFechas").css({ "width": "100%" });
        },
        toggleCheckbox: function () {
            $(".clsDates").change(function () {
                $("#btnGuardar").attr('disabled', false);
            });

        },
        duplicateInvoice_click: function () {
            var that = this,
           controls = that.getControls();

            $('#dgFechas').find('tbody').find('a[class="Duplicate"]').addEvent(that, 'click', that.getInvoice2);

        },
        getSelected: function () {
            var that = this;

            var rows = $('#dgFechas input[type="checkbox"]').map(function () {
                if ($(this).prop("checked")) {

                    var array = $(this).attr('name').split(";");

                    return that.getpathsInvoices(array[0], array[1], array[2]);
                }
            });

            return rows;
         
        },
        getDateVencimiento: function () {
            var that = this;
            var rows = $('#dgFechas input[type="checkbox"]').map(function () {
                if ($(this).prop("checked")) {
                    var array = $(this).attr('name').split(";");

                    return array[1];
                }
               
            });
            return rows;

        },
        getpathsInvoices: function (INVOICENUMBER, FECHA_VENCIMIENTO, PERIODO) {

            var that = this;
            that.result = "";
            $.app.ajax({
                async: false,
                type: "POST",
                dataType: "json",
                url: '/IFITransactions/CommonServices/GetFileRute',
                data: { strIdSession: Session.IDSESSION, strNumDocument: INVOICENUMBER, DateIssue: FECHA_VENCIMIENTO, Period: PERIODO },
                success: function (result) {

                    if (result != null) {
                        that.result = result.data;

                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert("La ruta del archivo no existe");
                }

            });
            return that.result;

        },
        getpathInvoice: function (send, args) {
            var that = this;
            var dataRowTM = that.dtFechas.row($(send).parents('td')).data();
            
            that.result = "";
            $.app.ajax({
                async: false,
                type: "POST",
                dataType: "json",
                url: '/IFITransactions/CommonServices/GetFileRute',
                data: { strIdSession: Session.IDSESSION, strNumDocument: dataRowTM.INVOICENUMBER, DateIssue: dataRowTM.FECHA_VENCIMIENTO, Period: dataRowTM.PERIODO },
                success: function (result) {
                    
                    if (result != null) {
                       
                        that.result = result.data;

                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert("La ruta del archivo no existe");
                }

            });
            return that.result;
        },
        getInvoice: function (send, args) {
            $("#btnGuardar").attr('disabled', false);
            var that = this;

            var result = that.getpathInvoice(send, args);
            
            if (result != null && result != "") {
               
                ReadRecordSharedFile(Session.IDSESSION, result);
            }
            else {
                alert("La ruta del archivo no existe");
            }
        },
        getInvoice2: function (send, args) {
            $("#btnGuardar").attr('disabled', false);
            var that = this;

            var result = that.getpathInvoice(send, args);

            if (result != null && result != "") {

                ReadRecordSharedInvoice(Session.IDSESSION, result);
            }
            else {
                alert("La ruta del archivo no existe");
            }
        },
        getBusinessRules: function (SubClaseCode) {
            var that = this, controls = that.getControls();
            controls.divReglas.empty().html('');
            $.app.ajax({
                type: "POST",
                url: "/IFITransactions/CommonServices/GetBusinessRules",
                data: {
                    strIdSession: Session.IDSESSION,
                    strSubClase: SubClaseCode
                },
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divReglas.html(list[0].REGLA);
                        }
                    }

                }
            });
        },

        ShowConstancy: function (filepath, Filename) {
            var that = this,
               controls = that.getControls();
            var url = that.strUrl + '/GenerateRecord/ExistFile';

            $.app.ajax({
                type: 'GET',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: url,
                data: { strFilePath: filepath, strFileName: Filename, strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.Exist == false) {
                        alert('No existe el Archivo.', "Alerta");
                    } else {
                        var url = that.strUrl + '/GenerateRecord/showInvoice';
                        window.open(url + "?strFilePath=" + Session.filepath + "&strFileName=" + Session.Filename + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION, "FACTURA ELECTRÓNICA", "");
                    }

                },
                error: function (ex) {
                    alert('No existe el Archivo.', "Alerta");
                }
            });
        },

        Round: function (cantidad, decimales) {

            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },


        pad: function (s) { return (s < 10) ? '0' + s : s; },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        loadPage: function () {
            $.blockUI({
                message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
        },


    };


    $.fn.INTDuplicateReceipts = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTDuplicateReceipts'),
                options = $.extend({}, $.fn.INTDuplicateReceipts.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTDuplicateReceipts', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }

        });
        return value || this;
    };

    $.fn.INTDuplicateReceipts.defaults = {
    }

    $('#divBody').INTDuplicateReceipts();
})(jQuery);