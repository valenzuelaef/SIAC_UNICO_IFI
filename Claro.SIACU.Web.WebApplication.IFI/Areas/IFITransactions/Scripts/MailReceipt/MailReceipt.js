var estadoE = "", ObjIdFin = "", estadoacc = "";
$(document).ready(function () {


    $('#txtNotes').keypress(function (event) {

        if (this.value.length === 3800) {
            return false;
        }
    });
    $("#txtTelfAfil").keydown(function (event) {
        if (event.shiftKey) {
            event.preventDefault();
        }

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (event.keyCode < 95) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    event.preventDefault();
                }
            }
            else {
                if (event.keyCode < 96 || event.keyCode > 105) {
                    event.preventDefault();
                }
            }
        }
    });

});
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTMailReceipt.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblTitle: $('#lblTitle', $element)
            , txtTipoDes: $('#txtTipoDes', $element)
            , txtClaseDes: $('#txtClaseDes', $element)
            , txtSubClaseDes: $('#txtSubClaseDes', $element)
            , txtEmail: $('#txtEmail', $element)
            , txtDominio: $('#txtDominio', $element)
            , ddlMotivo: $('#ddlMotivo', $element)
            , txtEmail2: $('#txtEmail2', $element)
            , txtDominio2: $('#txtDominio2', $element)
            , txtTelfAfil: $('#txtTelfAfil', $element)
            , chkEnviar: $('#chkEnviar', $element)
            , txtNotes: $('#txtNotes', $element)
            , btnSave: $('#btnSave', $element)
            , btnClose: $("#btnClose", $element)
            , btnConstancy: $("#btnConstancy", $element)
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
            , lblCustomerID: $('#lblCustomerID', $element)
            , lblDNI_RUC: $('#lblDNI_RUC', $element)
            , lblRepren_Legal: $('#lblRepren_Legal', $element)
            , lblCicloFact: $('#lblCicloFact', $element)
            , lblLimiteCred: $('#lblLimiteCred', $element)
            , ddlCACDAC: $("#ddlCACDAC", $element)
            , cboAccion: $("#cboAccion", $element)
            , chkResponsable: $("#chkResponsable", $element)
            , chkEnvio: $("#chkEnvio", $element)
            , txtEnviarCon: $('#txtEnviarCon', $element)
            , lblNroServicio: $('#lblNroServicio', $element)
            , lblCuenta: $('#lblCuenta', $element)
            , lblProducto: $('#lblProducto', $element)
            , lblTelRef: $('#lblTelRef', $element)
            , lblEmail: $('#lblEmail', $element)
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();

            controls.btnClose.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_Click);
            controls.chkEnviar.addEvent(that, 'change', that.chkEnviar_Click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);

            controls.txtTelfAfil.attr('maxlength',9);

            that.maximizarWindow();

            that.loadPage();
            that.render();
        },
        render: function () {

            var that = this,
            controls = that.getControls();
            controls.lblTitle.text("AFILIACIÓN A RECIBO POR CORREO ELECTRÓNICO");
            that.loadCustomerData();
            that.InitCacDaC();
            that.LoadEmail();
            that.GetNumberSMS();
            that.ValidateEmail();

            //that.ValidateAccion();
            controls.btnConstancy.prop('disabled', true);
        },
        Trim: function (String) {
            var that = this;
            if (String == null)
                return (false);

            return that.RTrim(that.LTrim(String));
        },
        LTrim: function (String) {
            var i = 0;
            var j = String.length - 1;

            if (String == null)
                return (false);

            for (i = 0; i < String.length; i++) {
                if (String.substr(i, 1) != ' ' &&
                    String.substr(i, 1) != '\t')
                    break;
            }

            if (i <= j)
                return (String.substr(i, (j + 1) - i));
            else
                return ('');
        },
        RTrim: function (String) {
            var i = 0;
            var j = String.length - 1;

            if (String == null)
                return (false);

            for (j = String.length - 1; j >= 0; j--) {
                if (String.substr(j, 1) != ' ' &&
                    String.substr(j, 1) != '\t')
                    break;
            }

            if (i <= j)
                return (String.substr(i, (j + 1) - i));
            else
                return ('');
        },
        getValue: function (id) {
            var that = this;
            var c = id;
            if (c != null) return that.Trim(c.val());
            return '';
        },

        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();

            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
            Session.ACCESO = SessionTransac.SessionParams.USERACCESS;


            Session.DATACUSTOMER =
            {
                ContratoID: Session.CLIENTE.ContractID,
                Country: Session.CLIENTE.LegalCountry,
                CustomerContact: Session.CLIENTE.CustomerContact,
                CustomerID: Session.CLIENTE.CustomerID,
                CustomerTypeCode: Session.CLIENTE.CodCustomerType,
                refencial: Session.CLIENTE.Reference,
                Departament_Fact: Session.CLIENTE.InvoiceDepartament,
                District: Session.CLIENTE.District,
                provincia: Session.CLIENTE.Province,
                Cuenta: Session.CLIENTE.Account,
                Ciclo_Facturacion: Session.CLIENTE.BillingCycle,
                Nro_Doc: Session.CLIENTE.DocumentNumber,
                NameCompleto: Session.CLIENTE.FullName,
                TipoCliente: Session.CLIENTE.CustomerType,
                Ubigueo: Session.CLIENTE.InstallUbigeo,
                RazonSocial: Session.CLIENTE.BusinessName,
                FechaActivacion: Session.CLIENTE.ActivationDate,
                Codigo_Plano_Fact: Session.CLIENTE.PlaneCodeBilling,
                Email: Session.CLIENTE.Email,
                Direccion_Despacho: Session.CLIENTE.Address,
                Urbanizacion_Legal: Session.CLIENTE.LegalUrbanization,
                Departamento_Legal: Session.CLIENTE.LegalDepartament,
                Distrito_Legal: Session.CLIENTE.LegalDistrict,
                Codigo_Plano_Inst: Session.CLIENTE.PlaneCodeInstallation,
                Country_Legal: Session.CLIENTE.LegalCountry,
                telefono_Referencial: Session.CLIENTE.PhoneReference,
                Codigo_Tipo_Customer: Session.CLIENTE.CodCustomerType,
                Departamento_Fact: Session.CLIENTE.InvoiceDepartament,
                Distrito_Fac: Session.CLIENTE.InvoiceDistrict,
                Country_Fac: Session.CLIENTE.InvoiceCountry,
                Provincia_Fac: Session.CLIENTE.InvoiceProvince,
                Objid_Site: Session.CLIENTE.SiteCode,
                RepresentLegal: Session.CLIENTE.LegalAgent,
                DNI_RUC: Session.CLIENTE.DNIRUC,
                TipoDocumento: Session.CLIENTE.DocumentType,
                PlaneCode: Session.CLIENTE.PlaneCodeInstallation,
                District_Fac: Session.CLIENTE.InvoiceDistrict,
                LegalUrbanization: Session.CLIENTE.LegalUrbanization,
                Segmento: Session.CLIENTE.Segment2,
                Telephone: Session.CLIENTE.Telephone
            }

            Session.DATALINEA =
            {
                NroCelular: Session.LINEA.CellPhone,
                StatusLinea: Session.LINEA.StateLine,
                Plan: Session.LINEA.Plan,
                Plazo_Contrato: Session.LINEA.TermContract,
                StateAgreement: Session.LINEA.StateAgreement,
            }

            Session.USERACCESS =
            {
                optionPermissions: Session.ACCESO.optionPermissions,
                CodigoPerfil: Session.ACCESO.sapVendorId,
                CodigoUsuario: Session.ACCESO.userId,
                NombreCompleto: Session.ACCESO.fullName,
                Login: Session.ACCESO.login,
            };

            //Constantes
            that.strVariableEmpty = "";
            //********** Datos del Cliente ***********/
            controls.lblNroServicio.html((Session.CLIENTE.Telephone == null) ? '' : Session.CLIENTE.Telephone);
            controls.lblContrato.html((Session.CLIENTE.ContractID == null) ? '' : Session.CLIENTE.ContractID);
            controls.lblCustomerID.html((Session.CLIENTE.CustomerID == null) ? '' : Session.CLIENTE.CustomerID);
            controls.lblCliente.html((Session.CLIENTE.BusinessName == null) ? '' : Session.CLIENTE.BusinessName);
            controls.lblContacto.html((Session.CLIENTE.FullName == null) ? '' : Session.CLIENTE.FullName);
            controls.lblDNI_RUC.html((Session.CLIENTE.DNIRUC == null) ? '' : Session.CLIENTE.DNIRUC);
            controls.lblRepren_Legal.html((Session.CLIENTE.LegalAgent == null) ? '' : Session.CLIENTE.LegalAgent);
            controls.lblProducto.html((Session.CLIENTE.ProductTypeText == null) ? '' : Session.CLIENTE.ProductTypeText);
            controls.lblCuenta.html((Session.DATACUSTOMER.Cuenta == null) ? '' : Session.DATACUSTOMER.Cuenta);
            controls.lblTelRef.html((Session.DATACUSTOMER.telefono_Referencial == null) ? '' : Session.DATACUSTOMER.telefono_Referencial);
            controls.lblEmail.html((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);
            //********** Direccíón de Facturación ***********/
            controls.lblDireccion.html((Session.CLIENTE.InvoiceAddress == null) ? '' : Session.CLIENTE.InvoiceAddress);
            controls.lblNotasDirec.html((Session.CLIENTE.Reference == null) ? '' : Session.CLIENTE.Reference);
            controls.lblPais.html((Session.CLIENTE.InvoiceCountry == null) ? '' : (Session.CLIENTE.InvoiceCountry).toUpperCase());
            controls.lblDepartamento.html((Session.DATACUSTOMER.Departament_Fact == null) ? '' : Session.DATACUSTOMER.Departament_Fact);
            controls.lblProvincia.html((Session.DATACUSTOMER.provincia == null) ? '' : Session.DATACUSTOMER.provincia);
            controls.lblDistrito.html((Session.DATACUSTOMER.District == null) ? '' : Session.DATACUSTOMER.District);
            controls.lblCodUbigeo.html((Session.CLIENTE.InvoiceCode == null) ? '' : Session.CLIENTE.InvoiceCode);
            controls.txtEnviarCon.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);
            console.log(SessionTransac.SessionParams);
            console.log(Session);
        },

        InitCacDaC: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {},
                parameters = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.Login;

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
                        data: JSON.stringify(objCacDacType),
                        url: '/IFITransactions/CommonServices/GetCacDacType',
                        success: function (response) {


                            controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },

        InitAccion: function () {

            var that = this,
           controls = that.getControls(),
           objLstAccionType = {};
            var acc = estadoacc;

            objLstAccionType.strIdSession = Session.IDSESSION;
            $('#cboAccion').empty();
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: that.strUrlAfil + '/GetAction',
                success: function (response) {

                    controls.cboAccion.append($('<option>', { value: '', html: 'Seleccionar' }));

                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboAccion.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                        });
                    }
                    console.log("get acc");
                    console.log(acc);
                    $("#cboAccion option[value=" + acc + "]").attr("selected", true);

                }
            });

        },

        LoadEmail: function () {
            var that = this,
                controls = that.getControls();
            if ($.IsEmail(Session.DATACUSTOMER.Email)) {
                var fullEmail = Session.DATACUSTOMER.Email;
                var arrFullEmail = fullEmail.split("@");
                controls.txtEmail.val(arrFullEmail[0]);
                controls.txtDominio.val(arrFullEmail[1]);
            }
        },

        GetNumberSMS: function () {

            var that = this,
           controls = that.getControls(),

            objLstAccionType = {};
            var id = "";
            objLstAccionType.strIdSession = Session.IDSESSION;
            objLstAccionType.Telefono = Session.CLIENTE.Telephone;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: that.strUrlAfil + '/GetObjContactId',
                success: function (response) {


                    var objcontactoid = response.data;
                    ObjIdFin = objcontactoid;
                    objLstAccionType.strObjId = objcontactoid;


                }
            });

        },

        ValidateEmail: function () {
            console.log("entro validar mail");
            var that = this,
           controls = that.getControls(),
           objLstAccionType = {};
            var st = "";
            var acf = 1;
            estadoacc = acf;
            objLstAccionType.strIdSession = Session.IDSESSION;
            objLstAccionType.CustomerCode = Session.DATACUSTOMER.CustomerID;

            controls.chkEnvio.prop('checked', false);
            controls.chkEnviar.prop('checked', true);

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: that.strUrlAfil + '/ValidateEmail',
                success: function (response) {


                    if (response.data != null) {
                        that.flagServicio = response.data;
                        st = response.data.toUpperCase();


                        if (response.data.toUpperCase() == "A") {
                            that.EnableFields(true);

                        } else {

                            that.InitAccion();
                        }

                    }
                    estadoE = st;
                    that.getTypification();
                }
            });

        },

        ValidateAccion: function () {
            console.log("entro accion");
            var that = this,
            controls = that.getControls();
            if (controls.chkEnvio.checked == true) {
                console.log("entro accion 2");
                console.log("entro accion 2333");
                $('#cboAccion').val(2);
            }
            else {
                console.log("entro accion 1");
                console.log("entro accion rrr");
                $('#cboAccion').val(1);
            }
        },

        getTypification: function () {
            //try {
            var that = this;
            var controls = that.getControls();
            var stateEmail = "";
            stateEmail = estadoE;

            //PageLoad
            var parameters = {
                strIdSession: Session.IDSESSION,
                strStateEmail: stateEmail

            };

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: that.strUrlAfil + '/getTypification',
                success: function (response) {

                    if (response.data != null) {
                        that.getLoadTypification(response.data);

                    }
                }
            });

        },

        TYPIFICATION: {
            ClaseId: "",
            SubClaseId: "",
            Tipo: "",
            ClaseDes: "",
            SubClaseDes: "",
            TipoId: "",
        },
        getLoadTypification: function (objTypification) {
            var that = this;

            that.TYPIFICATION.ClaseId = objTypification.CLASE_CODE;
            that.TYPIFICATION.SubClaseId = objTypification.SUBCLASE_CODE;
            that.TYPIFICATION.Tipo = objTypification.TIPO;
            that.TYPIFICATION.ClaseDes = objTypification.CLASE;
            that.TYPIFICATION.SubClaseDes = objTypification.SUBCLASE;
            that.TYPIFICATION.TipoId = objTypification.TIPO_CODE;

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

        Loading: function () {
            console.log("entro loading");
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

        btnSave_Click: function () {
            var that = this,
                controls = that.getControls();
            var motivo = $("#cboAccion option:selected").val();
            if (motivo == '') {
                alert('Seleccione una acción');
                return false;
            }

            var cac = $("#ddlCACDAC option:selected").val();
            if (cac == '') {
                alert('Seleccione el Punto de atención');
                return false;
            }

            var flagServicio = that.flagServicio;

            if (motivo == 1) motivo = 'A'; else motivo = 'D';
            if (flagServicio == motivo) {
                if (flagServicio == 'A') {
                    alert('Servicio Ya Se Encuentra Activado');
                    return false;
                } else {
                    alert('El Servicio Ya Se Encuentra Desactivado');
                    return false;
                };
            }
            if ($("#txtNotes").val().length > 3800) {
                alert('El campo no debe pasar los 3800 caracteres');
                return false;
            }

            if (motivo == 'A') {

                var email = that.getValue(controls.txtEmail).replace(/ñ/g, 'n').replace(/Ñ/g, 'N');
                var dominio = that.getValue(controls.txtDominio);
                var correo1 = email + '@' + dominio;

                var email2 = that.getValue(controls.txtEmail2).replace(/ñ/g, 'n').replace(/Ñ/g, 'N');;
                var dominio2 = that.getValue(controls.txtDominio2);
                var correo2 = email2 + '@' + dominio2;

                var caracteres = '@';
                var mensajeEmail = 'La dirección de E-mail autorizado y la dirección de confirmación E-mail no coinciden';
                var mensajeFormato = 'La dirección de E-mail autorizado no tiene el formato correcto para el registro';
                var mensajeFormato2 = 'La dirección de confirmación E-mail no tienen el formato correcto para el registro';

                if (email == '' || dominio == '') {
                    alert(mensajeEmail);
                    return false
                } else {
                    if (caracteres.indexOf(correo1) != -1) {
                        alert(mensajeFormato);
                        return false;
                    }
                    if (!$.IsEmail(correo1)) {
                        alert(mensajeFormato);
                        return false;
                    }
                }

                if (email2 == '' || dominio2 == '') {
                    alert(mensajeEmail);
                    return false;
                } else {
                    if (caracteres.indexOf(correo2) != -1) {
                        alert(mensajeFormato2);
                        return false;
                    }
                    if (!$.IsEmail(correo2)) {
                        alert(mensajeFormato2);

                        return false;
                    }
                }

                if (correo1 != correo2) {
                    alert(mensajeEmail);
                    return false;
                }

                var emailCliente = Session.DATACUSTOMER.Email;
                if (emailCliente != correo1) {
                    that.FlagEmail = 'S';
                }

                var enviarmail = that.getValue(controls.txtEnviarCon);
                if (enviarmail == '') {
                    alert('Es necesario ingresar una dirección de correo para el envío de Constancia.');
                    return false
                } else {
                    if (!$.IsEmail(enviarmail)) {
                        alert('Es necesario ingresar una dirección de correo válido para el envío de Constancia.');
                    return false;
                }
                }
            }

                if (motivo == 'D') {
                var enviarmail = that.getValue(controls.txtEnviarCon);
                if (enviarmail == '') {
                    alert('Es necesario ingresar una dirección de correo para el envío de Constancia.');
                    return false
                } else {
                    if (!$.IsEmail(enviarmail)) {
                        alert('Es necesario ingresar una dirección de correo válido para el envío de Constancia.');
                        return false;
                    }
                }

            }

            var objResultado = {};
            var intResultado;
            var Opcion = $("#cboAccion option:selected").val() == "1" ? "A" : "D";
            that.flagServicio = (Opcion == "A") ? "X" : "";

            that.Loading();

            that.EjecutaActivacion(that.flagServicio, Opcion);

        },
        isValidEmail: function (_email) {
            return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(_email);
        },

        strMsjEmail: "",

        idInteraction: "",

        SaveInteraction: function () {
            var that = this,
            controls = that.getControls(),
            strUrl = '',
            model = {};

            model.IdSession = Session.IDSESSION;
            model.NameComplet = Session.DATACUSTOMER.NameCompleto;
            model.NroDoc = Session.DATACUSTOMER.Nro_Doc;
            model.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
            model.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            model.Reference = Session.DATACUSTOMER.refencial;
            model.Departament_Fact = Session.DATACUSTOMER.Departamento_Fact;
            model.District = Session.DATACUSTOMER.District_Fac;
            model.Provincia = Session.DATACUSTOMER.Provincia_Fac;
            model.ContractId = Session.DATACUSTOMER.ContratoID;
            model.CustomerId = Session.DATACUSTOMER.CustomerID;
            model.TelephoneSMS = controls.txtTelfAfil.val();
            model.StateMail = $("#cboAccion option:selected").val() == "1" ? "A" : "D";
            model.CacDac = $('#ddlCACDAC option:selected').text();
            model.Telephone = Session.DATACUSTOMER.Telephone;
            model.Email = $("#txtEmail").val() + "@" + $("#txtDominio").val();
            model.Note = controls.txtNotes.val();
            model.CurrentUser = Session.USERACCESS.Login;
            model.EmailCond = $('#txtEnviarCon').val();
            model.EmailCondstate = controls.chkEnviar.is(':checked');
            model.FullNameUser = Session.USERACCESS.NombreCompleto;
            model.RepresentLegal = Session.DATACUSTOMER.RepresentLegal;
            model.CustomerType = Session.CLIENTE.CustomerType;
            model.TelefonoReferencia = Session.DATACUSTOMER.telefono_Referencial;
            model.ObjIdContact = ObjIdFin;

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: that.strUrlAfil + '/SaveInteractionMail',
                error: function (data) {
                    alert(that.strMensajeDeError, "Alerta");
                    $("#btnSave").attr('disabled', true);
                    $("#btnConstancy").attr('disabled', true);
                    $("#txtNotes").attr('disabled', true);
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

                    vDesInteraction = response.vDesInteraction;
                    InteractionId = response.vInteractionId;
                    RutaArchivo = response.strRutaArchivo;
                    stremail = response.MensajeEmail;
                    if (that.InteractionId != '' || that.InteractionId != null) {
                        $("#btnConstancy").attr('disabled', false);
                        $("#btnSave").attr('disabled', true);
                        $("#txtNotes").attr('disabled', true);
                        alert("La transacción se realizó con éxito." + stremail, "Informativo");


                    } else {
                        alert("No se pudo Guardar la Transaccion", "Alerta");
                    }
                },
                complete: function () {
                    $.unblockUI();

                }
            });


        },

        InteractionId: "",

        RutaArchivo: "",

        vDesInteraction: "",

        stremail: "",

        message: "",

        EjecutaActivacion: function (flagServicio, Opcion) {
            var that = this,
            controls = that.getControls(),
            objRequest = {};
            var ObContId = ObjIdFin;

            //ACTIVACION/DESACTIVACION
            objRequest.strIdSession = Session.IDSESSION;
            objRequest.pCuenta = Session.DATACUSTOMER.Cuenta;
            objRequest.pCustomerID = Session.DATACUSTOMER.CustomerID;
            objRequest.pEmail = that.Trim(controls.txtEmail.val()) + "@" + that.Trim(controls.txtDominio.val());
            objRequest.pFlag = flagServicio;
            objRequest.pTelRef = Session.DATACUSTOMER.telefono_Referencial;
            objRequest.pNumCla = Session.DATACUSTOMER.Telephone;
            objRequest.pObjID = ObContId;
            objRequest.pTelConfSMS = Session.DATACUSTOMER.telefono_Referencial;
            objRequest.Opcion = Opcion;
            objRequest.email = $.trim(controls.txtEmail.val());
            objRequest.dominio = $.trim(controls.txtDominio.val());

            //INTERACCION
            oModel = {};

            oModel.IdSession = Session.IDSESSION;
            oModel.NameComplet = Session.DATACUSTOMER.NameCompleto;
            oModel.NroDoc = Session.DATACUSTOMER.Nro_Doc;
            oModel.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
            oModel.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            oModel.Reference = Session.DATACUSTOMER.refencial;
            oModel.Departament_Fact = Session.DATACUSTOMER.Departamento_Fact;
            oModel.District = Session.DATACUSTOMER.District_Fac;
            oModel.Provincia = Session.DATACUSTOMER.Provincia_Fac;
            oModel.ContractId = Session.DATACUSTOMER.ContratoID;
            oModel.CustomerId = Session.DATACUSTOMER.CustomerID;
            oModel.TelephoneSMS = Session.DATACUSTOMER.telefono_Referencial
            oModel.StateMail = $("#cboAccion option:selected").val() == "1" ? "A" : "D";
            oModel.CacDac = $('#ddlCACDAC option:selected').text();
            oModel.Telephone = Session.DATACUSTOMER.Telephone;
            oModel.Email = $("#txtEmail").val() + "@" + $("#txtDominio").val();
            oModel.Note = controls.txtNotes.val();
            oModel.CurrentUser = Session.USERACCESS.Login;
            oModel.EmailCond = $('#txtEnviarCon').val();
            oModel.EmailCondstate = controls.chkEnviar.is(':checked');
            oModel.FullNameUser = Session.USERACCESS.NombreCompleto;
            oModel.RepresentLegal = Session.DATACUSTOMER.RepresentLegal;
            oModel.CustomerType = Session.CLIENTE.CustomerType;
            oModel.TelefonoReferencia = Session.DATACUSTOMER.telefono_Referencial;
            oModel.ObjIdContact = ObjIdFin;
            objRequest.oModel = oModel;

           

            $.app.ajax({
                type: 'POST',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequest),
                url: that.strUrlAfil + '/GetActivateServiceMail',
                error: function (data) {
                    alert(that.strMensajeDeError, "Alerta");
                    $("#btnSave").attr('disabled', true);
                    $("#btnConstancy").attr('disabled', true);
                    $("#txtNotes").attr('disabled', true);
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
                    InteractionId = response.vInteractionId;
                    stremail = response.MensajeEmail;

                    if (InteractionId != '' && InteractionId != null) {
                        $("#btnConstancy").attr('disabled', false);
                        $("#btnSave").attr('disabled', true);
                        $("#txtNotes").attr('disabled', true);
                        alert("La transacción se realizó con éxito." + stremail, "Informativo");


                    } else {
                        alert("No se pudo Guardar la Transaccion", "Alerta");
                    }
                },
                complete: function () {
                    $.unblockUI();

                }
            });
         
        },

        FlagEmail: "",

        modelMailReceipt: {},

        SetValues: function () {
            var that = this,
               controls = that.getControls();
            if (Session.CLIENTE.objPostDataAccount.ResponsiblePayment == "X") {
                controls.chkResponsable.prop("checked", true);
            } else {
                controls.chkResponsable.prop("checked", false);
            }

        },

        EnableFields: function (state) {
            var that = this,
                controls = that.getControls();
            controls.txtEmail.prop('disabled', state);
            controls.txtDominio.prop('disabled', state);
            controls.txtEmail2.prop('disabled', state);
            controls.txtDominio2.prop('disabled', state);
            controls.txtTelfAfil.prop('disabled', state);
            controls.chkEnvio.prop('checked', state);
            estadoacc = 2;
            console.log("estado acc2")
            that.InitAccion();

        },

        btnConstancy_click: function () {

            var that = this;
            var PDFRoute = RutaArchivo;
            var IdSession = Session.IDSESSION;

            if (PDFRoute != "") {

                ReadRecordSharedFile(IdSession, PDFRoute);
            }
        },

        flagServicio: "",

        strUrlAfil: '/IFITransactions/MailReceipt',

        btnCerrar_Click: function () {
            parent.window.close();
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },
        maximizarWindow: function () {

            window.moveTo(0, 0);
            window.resizeTo(screen.width, screen.height);

        },

        chkEnviar_Click: function () {
            var objCheck = document.getElementById('chkEnviar');
            var objEmail = document.getElementById('txtEnviarCon');
            if (objCheck.checked == true) {
                objEmail.style.display = "";
            }
            else {
                objEmail.style.display = "none";
            }
        },

    }

    $.fn.INTMailReceipt = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTMailReceipt'),
                options = $.extend({}, $.fn.INTMailReceipt.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTMailReceipt', data);
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

    $.fn.INTMailReceipt.defaults = {
    }

    $('#divBody').INTMailReceipt();

})(jQuery);
