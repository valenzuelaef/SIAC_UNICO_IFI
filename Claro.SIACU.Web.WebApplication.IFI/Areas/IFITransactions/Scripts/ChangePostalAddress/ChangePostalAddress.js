var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};
var mto, fec, intLon, strReferencia, Enviomail, idpais, iddep, idprov, iddist;

$(document).ready(function () {
    $("#txtNumeroCalle").keydown(function (event) {
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
    $(document).ready(function () {
        $("#txtNote").on('paste', function (e) {
            e.preventDefault();
                })

    });
});
(function ($, undefined) {

    var RutaArchivo = "";

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTChangePostalAddress.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            //Titulo
             , lblTitle: $('#lblTitle', $element)
            , cboCacDac: $("#cboCacDac", $element)
            , cboPaisMod: $("#cboPaisMod", $element)
            , cboDepMod: $("#cboDepMod", $element)
            , cboProvinciaMod: $("#cboProvinciaMod", $element)
            , cboDistritoMod: $("#cboDistritoMod", $element)
            , txtCodPostalMod: $("#txtCodPostalMod", $element)
            //textbox
            , txtEmail: $("#txtEmail", $element)
            , txtNote: $('#txtNote', $element)
            //label : Datos cliente
            , lblNroTelefono: $("#lblNroTelefono", $element)
            , lblContrato: $("#lblContrato", $element)
            , lblCustomerId: $("#lblCustomerId", $element)
            , lblPlan: $("#lblPlan", $element)
            , lblRepreLegal: $("#lblRepreLegal", $element)
            , lblTipoCliente: $("#lblTipoCliente", $element)
            , lblContacto: $("#lblContacto", $element)
            , lblCliente: $("#lblCliente", $element)
            , lblFecAct: $("#lblFecAct", $element)
            , lblDniRuc: $('#lblDniRuc', $element)
            , lblCicloFact: $('#lblCicloFact', $element)
            , lblDireccion: $("#lblDireccion", $element)
            , lblNotasDirec: $("#lblNotasDirec", $element)
            , lblCodPostal: $('#lblCodPostal', $element)
            , lblPais: $("#lblPais", $element)
            , lblDepartamento: $("#lblDepartamento", $element)
            , lblProvincia: $("#lblProvincia", $element)
            , lblDistrito: $("#lblDistrito", $element)
            , chkEmail: $("#chkEmail", $element)
            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnConstancia: $("#btnConstancia", $element)
            , myModalLoad: $("#myModalLoad", $element)
            , lblErrorMessage: $("#lblErrorMessage", $element)
            , divReglas: $("#divReglas", $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , cboTipoVia: $("#cboTipoVia", $element)
            , txtVia: $("#txtVia", $element)
            , txtNumeroCalle: $("#txtNumeroCalle", $element)
            , chkSN: $("#chkSN", $element)
            , cboTipoMz: $("#cboTipoMz", $element)
            , txtNroMz: $("#txtNroMz", $element)
            , txtLote: $("#txtLote", $element)
            , cboTipoInterior: $("#cboTipoInterior", $element)
            , txtInterior: $("#txtInterior", $element)
            , txtContadorD1: $("#txtContadorD1", $element)
            , cboTipoUrb: $("#cboTipoUrb", $element)
            , txtUrb: $("#txtUrb", $element)
            , cboTipoZona: $("#cboTipoZona", $element)
            , txtZona: $("#txtZona", $element)
            , txtReferencia: $("#txtReferencia", $element)
            , txtContadorD2: $("#txtContadorD2", $element)
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
            controls.chkEmail.addEvent(that, 'change', that.MostrarOcultarEmail);
            controls.cboPaisMod.addEvent(that, 'change', that.InitValidaPais);
            controls.cboDepMod.addEvent(that, 'change', that.InitProvincias);
            controls.cboProvinciaMod.addEvent(that, 'change', that.InitDistritos);
            controls.cboDistritoMod.addEvent(that, 'change', that.InitPostalCode);

            controls.cboTipoVia.addEvent(that, 'change', that.ContadorD1);
            controls.txtVia.addEvent(that, 'change', that.ContadorD1);
            controls.txtNumeroCalle.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoMz.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoMz.addEvent(that, 'change', that.ContadorD1);
            controls.txtNroMz.addEvent(that, 'change', that.ContadorD1);
            controls.txtLote.addEvent(that, 'change', that.ContadorD1);
            controls.chkSN.addEvent(that, 'change', that.CheckSiNo);
            controls.cboTipoInterior.addEvent(that, 'change', that.ContadorD1);
            controls.txtInterior.addEvent(that, 'change', that.ContadorD1);

            controls.cboTipoUrb.addEvent(that, 'change', that.ContadorD2);
            controls.txtUrb.addEvent(that, 'change', that.ContadorD2);
            controls.cboTipoZona.addEvent(that, 'change', that.ContadorD2);
            controls.txtZona.addEvent(that, 'change', that.ContadorD2);
            controls.txtReferencia.addEvent(that, 'change', that.ContadorD2);

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },

        modelChangePostalAddress: {},

        render: function () {

            var that = this,
            control = that.getControls();
            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);
            control.divErrorAlert.hide();
            that.loadCustomerData();
            that.InitLastInvoiceData();
            that.InitVias();
            that.InitManzanas();
            that.InitInteriores();
            that.InitUrbs();
            that.InitZonas();
            that.InitCacDaCSesion();
            that.loadTypification();

            control.chkEmail.attr("checked", true);
            if (control.chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
            that.getRulesControls();
        },

        loadTypification: function () {

            var obj = { strIdSession: Session.IDSESSION };
            $.app.ajax({
                type: 'POST',
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangePostalAddress/PageLoad',
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
            controls.lblTitle.text("Cambio de Direccion de Facturación");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
            Session.ACCESO = SessionTransac.SessionParams.USERACCESS;

            //********** Datos del Cliente ***********/
            controls.lblNroTelefono.html((Session.CLIENTE.Telephone == null) ? '' : Session.CLIENTE.Telephone);
            controls.lblTipoCliente.html((Session.CLIENTE.CustomerType == null) ? '' : Session.CLIENTE.CustomerType);
            controls.lblContacto.html((Session.CLIENTE.FullName == null) ? '' : Session.CLIENTE.FullName);
            controls.lblCliente.html((Session.CLIENTE.BusinessName == null) ? '' : Session.CLIENTE.BusinessName);
            controls.lblContrato.html((Session.CLIENTE.ContractID == null) ? '' : Session.CLIENTE.ContractID);
            controls.lblCustomerId.html((Session.CLIENTE.CustomerID == null) ? '' : Session.CLIENTE.CustomerID);
            controls.lblPlan.html((Session.LINEA.Plan == null) ? '' : Session.LINEA.Plan);
            controls.lblRepreLegal.html((Session.CLIENTE.LegalAgent == null) ? '' : Session.CLIENTE.LegalAgent);
            controls.lblFecAct.html((Session.LINEA.ActivationDate == null) ? '' : Session.LINEA.ActivationDate);
            //********** Direccíón de Facturación ***********/
            controls.lblDniRuc.html((Session.CLIENTE.DNIRUC == null) ? '' : Session.CLIENTE.DNIRUC);
            controls.lblCicloFact.html((Session.CLIENTE.objPostDataAccount.BillingCycle == null) ? '' : Session.CLIENTE.objPostDataAccount.BillingCycle);
            controls.lblDireccion.html((Session.CLIENTE.Address == null) ? '' : Session.CLIENTE.Address);
            controls.lblNotasDirec.html((Session.CLIENTE.Reference == null) ? '' : Session.CLIENTE.Reference);
            controls.lblPais.html((Session.CLIENTE.InvoiceCountry == null) ? '' : (Session.CLIENTE.InvoiceCountry).toUpperCase());
            controls.lblDepartamento.html((Session.CLIENTE.Departament == null) ? '' : Session.CLIENTE.Departament);
            controls.lblProvincia.html((Session.CLIENTE.Province == null) ? '' : Session.CLIENTE.Province);
            controls.lblDistrito.html((Session.CLIENTE.District == null) ? '' : Session.CLIENTE.District);
            controls.txtEmail.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
            idpais = Session.CLIENTE.InvoiceCountry.toUpperCase();
            iddep = Session.CLIENTE.Departament.toUpperCase();
            idprov = Session.CLIENTE.Province.toUpperCase();
            iddist = Session.CLIENTE.District.toUpperCase();
            that.InitNacionalidad();


            $("#txtVia").keypress(function (e) {
                var regex = new RegExp(/^[0-9a-zA-Z\s.,]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $("#txtNroMz").keypress(function (e) {
                var regex = new RegExp(/^[0-9a-zA-Z\s.,]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $("#txtLote").on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });

            $("#txtInterior").keypress(function (e) {
                var regex = new RegExp(/^[0-9a-zA-Z\s.,]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $("#txtZona").keypress(function (e) {
                var regex = new RegExp(/^[0-9a-zA-Z\s.,]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });
            $("#txtUrb").keypress(function (e) {
                var regex = new RegExp(/^[0-9a-zA-Z\s.,]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });
            $("#txtNote").keypress(function (e) {
                var regex = new RegExp(/^[0-9a-zA-Z\s.,]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });
            $("#txtReferencia").keypress(function (e) {
                var regex = new RegExp(/^[0-9a-zA-Z\s.,]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });


        },

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

        btnGuardar_click: function () {
            if ($('#frmChangePostalAddress').valid()) {
                var that = this,
                    controls = this.getControls();

                that.isPostBackFlag = that.NumeracionUNO;

                if (!that.validarMaxCaracteres()) {
                    return false;
                }

                if (!that.validarMinCaracteres2()) {
                    return false;
                }

                if (!that.validarMaxCaracteres2()) {
                    return false;
                }

                if (!that.valida_direccion()) {
                    return false;
                }

                if (!that.ValidateControl()) {
                    return false;
                }

                if (!that.validarMaxCaracteresNote()) {
                    return false;
                }
                confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {
                    that.Loading();
                    that.SaveTransactionChangePostalA();

                }, function () {
                    $("#hidAccion").val("");
                    return false;
                });
            }
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

        ValidateControl: function () {
            var that = this,
                control = that.getControls();

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

            control.chkEmail.prop('disabled', true);
        },

        SaveTransactionChangePostalA: function () {

            var that = this,
                controls = that.getControls(),
                chkEmail = controls.chkEmail;
            var enviog = "";

            that.modelChangePostalAddress.strIdSession = Session.IDSESSION;
            that.modelChangePostalAddress.strCustomerId = Session.CLIENTE.CustomerID;
            that.modelChangePostalAddress.strMailChange = $('#txtEmail').val();
            that.modelChangePostalAddress.strDireccion = ($('#txtDireccion').val()).toUpperCase();
            that.modelChangePostalAddress.strReferencia = strReferencia.toUpperCase();
            that.modelChangePostalAddress.strPais = $('#cboPaisMod option:selected').text();
            that.modelChangePostalAddress.strDepartamento = $('#cboDepMod option:selected').text();
            that.modelChangePostalAddress.strProvincia = $('#cboProvinciaMod option:selected').text();
            that.modelChangePostalAddress.strDistrito = $('#cboDistritoMod option:selected').text();
            that.modelChangePostalAddress.strCodPostal = $('#txtCodPostalMod').val();
            that.modelChangePostalAddress.strNote = $("#txtNote").val();
            that.modelChangePostalAddress.chkEmail = controls.chkEmail.is(':checked');
            that.modelChangePostalAddress.strCacDac = $('#cboCacDac option:selected').text();

            if (chkEmail[0].checked == true) {
                that.modelChangePostalAddress.Flag_Email = true;
                enviog = "S";
            } else {
                that.modelChangePostalAddress.Flag_Email = false;
                enviog = "N";
            }
            Enviomail = enviog;
            that.modelChangePostalAddress.strObjidContacto = Session.CLIENTE.IdContactObject;
            that.modelChangePostalAddress.strNombres = Session.CLIENTE.Nombres;
            that.modelChangePostalAddress.strApellidos = Session.CLIENTE.Apellidos;
            that.modelChangePostalAddress.strTelefono = Session.CLIENTE.Telephone;

            that.modelChangePostalAddress.account = Session.CLIENTE.Account;
            that.modelChangePostalAddress.contractId = Session.CLIENTE.ContractID;
            that.modelChangePostalAddress.currentUser = Session.ACCESO.login;
            that.modelChangePostalAddress.customerId = Session.CLIENTE.CustomerID;
            that.modelChangePostalAddress.tipo = TYPIFICATION.Tipo;
            that.modelChangePostalAddress.claseDes = TYPIFICATION.ClaseDes;
            that.modelChangePostalAddress.subClaseDes = TYPIFICATION.SubClaseDes;
            that.modelChangePostalAddress.claseCode = TYPIFICATION.ClaseId;
            that.modelChangePostalAddress.subClaseCode = TYPIFICATION.SubClaseId;
            that.modelChangePostalAddress.tipoCode = TYPIFICATION.TipoId;
            //extras prueba constancia
            that.modelChangePostalAddress.fullNameUser = Session.ACCESO.fullName;
            that.modelChangePostalAddress.strNameComplet = Session.CLIENTE.FullName;
            that.modelChangePostalAddress.RepresentLegal = Session.CLIENTE.LegalAgent;
            that.modelChangePostalAddress.DNI_RUC = Session.CLIENTE.DocumentNumber;
            that.modelChangePostalAddress.strTipoDocumento = Session.CLIENTE.DocumentType;



            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelChangePostalAddress),
                url: '/IFITransactions/ChangePostalAddress/SaveTransactionChangePostalAddress',
                error: function (data) {
                    alert(that.strMensajeDeError, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);
                },
                success: function (response) {
                    RutaArchivo = response.strRutaArchivo;
                    that.vDesInteraction = response.vDesInteraction;
                    $("#btnConstancia").attr('disabled', false);
                    alert(that.vDesInteraction, "Informativo");

                    that.BlockControl();
                    $("#btnGuardar").attr('disabled', true);
                }
            });
        },

        btnConstancia_click: function () {

            var that = this;
            var PDFRoute = RutaArchivo;
            var IdSession = Session.IDSESSION;

            if (PDFRoute != "") {

                ReadRecordSharedFile(IdSession, PDFRoute);
            }
        },

        InitCacDaCSesion: function (pid) {

            if (pid == null || pid.toString == "undefined") { pid = ""; }

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
                        data: JSON.stringify(objCacDacType),
                        url: '/IFITransactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboCacDac.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboCacDac option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboCacDac option[value=" + pid + "]").attr("selected", true); }

                            if (cacdac != '') {
                                $("#cboCacDac option:contains(" + cacdac + ")").attr('selected', true);
                            }
                        }
                    });
                }
            });
        },

        InitNacionalidad: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objNacType = {},
                parameters = {};

            objNacType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Nac = results.NACIONALIDAD;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objNacType),
                        url: '/IFITransactions/CommonServices/GetNacType',
                        success: function (response) {
                            controls.cboPaisMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.NacTypes, function (index, value) {

                                if (Nac === value.Description) {
                                    controls.cboPaisMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboPaisMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboPaisMod option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboPaisMod option[value=" + pid + "]").attr("selected", true); }
                            ((idpais != 'PERU') ? controls.cboPaisMod.val('0') : controls.cboPaisMod.val('154'));
                            ((idpais != 'PERU') ? controls.cboPaisMod.prop('disabled', false) : controls.cboPaisMod.prop('disabled', true));
                            that.InitDepartamentosSesion();
                        }
                    });
                }
            });
        },

        InitVias: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objViasType = {},
                parameters = {};

            objViasType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Via = results.TIPO_VIA_DESC;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objViasType),
                        url: '/IFITransactions/CommonServices/GetViaType',
                        success: function (response) {
                            controls.cboTipoVia.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.ViaTypes, function (index, value) {

                                if (Via === value.Description) {
                                    controls.cboTipoVia.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboTipoVia.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboTipoVia option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboTipoVia option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitManzanas: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objManzanasType = {},
                parameters = {};

            objManzanasType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Manzana = results.TIPO_MANZANA_DESC;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objManzanasType),
                        url: '/IFITransactions/CommonServices/GetManzanaType',
                        success: function (response) {
                            controls.cboTipoMz.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.ManzanaTypes, function (index, value) {

                                if (Manzana === value.Description) {
                                    controls.cboTipoMz.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboTipoMz.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboTipoMz option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboTipoMz option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitInteriores: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objInterioresType = {},
                parameters = {};

            objInterioresType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Interior = results.DESC_TIPO_INTERIOR;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objInterioresType),
                        url: '/IFITransactions/CommonServices/GetInteriorType',
                        success: function (response) {
                            controls.cboTipoInterior.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.InteriorTypes, function (index, value) {

                                if (Interior === value.Description) {
                                    controls.cboTipoInterior.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboTipoInterior.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboTipoInterior option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboTipoInterior option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitUrbs: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUrbsType = {},
                parameters = {};

            objUrbsType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Urb = results.DESC_TIPO_URBANIZACION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUrbsType),
                        url: '/IFITransactions/CommonServices/GetUrbType',
                        success: function (response) {
                            controls.cboTipoUrb.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UrbTypes, function (index, value) {

                                if (Urb === value.Description) {
                                    controls.cboTipoUrb.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboTipoUrb.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboTipoUrb option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboTipoUrb option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitZonas: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objZonasType = {},
                parameters = {};

            objZonasType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Zone = results.DESC_TIPO_ZONA;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objZonasType),
                        url: '/IFITransactions/CommonServices/GetZoneType',
                        success: function (response) {
                            controls.cboTipoZona.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.ZoneTypes, function (index, value) {

                                if (Zone === value.Description) {
                                    controls.cboTipoZona.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboTipoZona.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboTipoZona option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboTipoZona option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitValidaPais: function () {

            var that = this,
                controls = that.getControls();
            if (controls.cboPaisMod.val() == '154') {
                controls.cboDepMod.prop('disabled', false);
                that.InitDepartamentos();
            }
            else {
                controls.cboDepMod.val('0');
                controls.cboDepMod.prop('disabled', true);
                controls.cboProvinciaMod.val('0');
                controls.cboProvinciaMod.prop('disabled', true);
                controls.cboDistritoMod.val('0');
                controls.cboDistritoMod.prop('disabled', true);
                controls.txtCodPostalMod.val('');
            }
        },

        InitDepartamentos: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos1Type = {},
                parameters = {};

            objUbigeos1Type.strIdSession = Session.IDSESSION;
            objUbigeos1Type.dep = 0;
            objUbigeos1Type.prov = 0;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo1 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos1Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboDepMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (Ubigeo1 === value.Description) {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboDepMod option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboDepMod option[value=" + pid + "]").attr("selected", true); }
                            controls.cboProvinciaMod.prop('disabled', true);
                            controls.cboDistritoMod.prop('disabled', true);
                            controls.cboDistritoMod.val('');
                        }
                    });
                }
            });
        },

        InitDepartamentosSesion: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos1Type = {},
                parameters = {};

            objUbigeos1Type.strIdSession = Session.IDSESSION;
            objUbigeos1Type.dep = 0;
            objUbigeos1Type.prov = 0;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo1 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos1Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboDepMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (Ubigeo1 === value.Description) {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboDepMod option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboDepMod option[value=" + pid + "]").attr("selected", true); }

                            if (idpais != 'PERU') {
                                $("#cboDepMod").prop("disabled", true);
                            } else {
                                $("#cboDepMod option:contains(" + iddep + ")").attr('selected', true);
                            }
                            that.InitProvinciasSesion();
                            controls.cboProvinciaMod.prop('disabled', true);
                            controls.cboDistritoMod.prop('disabled', true);
                        }
                    });
                }
            });
        },

        InitProvincias: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos2Type = {},
                parameters = {};
            objUbigeos2Type.dep = controls.cboDepMod.val();
            if (objUbigeos2Type.dep == '') {
                controls.cboProvinciaMod.empty();
                controls.cboDistritoMod.empty();
                controls.cboProvinciaMod.prop('disabled', true);
                controls.cboDistritoMod.prop('disabled', true);
                controls.txtCodPostalMod.val('');
            } else {

                objUbigeos2Type.strIdSession = Session.IDSESSION;
                objUbigeos2Type.dep = controls.cboDepMod.val();
                objUbigeos2Type.prov = 0;

                parameters.strIdSession = Session.IDSESSION;
                parameters.strCodeUser = Session.ACCESO.login;

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(parameters),
                    url: '/IFITransactions/CommonServices/GetUsers',
                    success: function (results) {
                        var Ubigeo2 = results.DESCRIPCION;
                        $.ajax({
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            data: JSON.stringify(objUbigeos2Type),
                            url: '/IFITransactions/CommonServices/GetUbigeoType',
                            success: function (response) {
                                controls.cboProvinciaMod.empty();
                                controls.cboDistritoMod.empty();
                                controls.cboProvinciaMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                                if (response.data != null) { }
                                var itemSelect;
                                $.each(response.data.UbigeoTypes, function (index, value) {

                                    if (Ubigeo2 === value.Description) {
                                        controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                        itemSelect = value.Code;

                                    } else {
                                        controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    }
                                });
                                if (itemSelect != null && itemSelect.toString != "undefined") {
                                    $("#cboProvinciaMod option[value=" + itemSelect + "]").attr("selected", true);
                                }
                            }
                        });
                    }
                });
                controls.cboProvinciaMod.prop('disabled', false);
                controls.cboDistritoMod.prop('disabled', true);
                controls.txtCodPostalMod.val('');
            }
        },

        InitProvinciasSesion: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos2Type = {},
                parameters = {};

            objUbigeos2Type.strIdSession = Session.IDSESSION;
            objUbigeos2Type.dep = controls.cboDepMod.val();
            objUbigeos2Type.prov = 0;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo2 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos2Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboProvinciaMod.empty();
                            controls.cboDistritoMod.empty();
                            controls.cboProvinciaMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (Ubigeo2 === value.Description) {
                                    controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboProvinciaMod option[value=" + itemSelect + "]").attr("selected", true);
                            }
                            if (idpais != 'PERU') {
                                $("#cboProvinciaMod").prop("disabled", true);
                            } else {
                                $("#cboProvinciaMod").prop("disabled", false);
                                $("#cboProvinciaMod option:contains(" + idprov + ")").attr('selected', true);
                            }
                            that.InitDistritosSesion();
                            that.getRulesUbigeoProv();
                            that.getRulesUbigeoDist();
                        }
                    });
                }
            });
            controls.cboDistritoMod.prop('disabled', true);
        },

        InitDistritos: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos3Type = {},
                parameters = {};

            objUbigeos3Type.prov = controls.cboProvinciaMod.val();
            if (objUbigeos3Type.prov == '') {
                controls.cboDistritoMod.empty();
                controls.cboDistritoMod.prop('disabled', true);
                controls.txtCodPostalMod.val('');
            } else {

                objUbigeos3Type.strIdSession = Session.IDSESSION;
                objUbigeos3Type.dep = controls.cboDepMod.val();
                objUbigeos3Type.prov = controls.cboProvinciaMod.val();

                parameters.strIdSession = Session.IDSESSION;
                parameters.strCodeUser = Session.ACCESO.login;

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(parameters),
                    url: '/IFITransactions/CommonServices/GetUsers',
                    success: function (results) {
                        var Ubigeo3 = results.DESCRIPCION;
                        $.ajax({
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            data: JSON.stringify(objUbigeos3Type),
                            url: '/IFITransactions/CommonServices/GetUbigeoType',
                            success: function (response) {
                                controls.cboDistritoMod.empty();
                                controls.cboDistritoMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                                if (response.data != null) { }
                                var itemSelect;
                                $.each(response.data.UbigeoTypes, function (index, value) {

                                    if (Ubigeo3 === value.Description) {
                                        controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                        itemSelect = value.Code;

                                    } else {
                                        controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    }
                                });
                                if (itemSelect != null && itemSelect.toString != "undefined") {
                                    $("#cboDistritoMod option[value=" + itemSelect + "]").attr("selected", true);
                                }
                            }
                        });
                    }
                });
                controls.cboDistritoMod.prop('disabled', false);
                that.getRulesUbigeoDist();
                controls.txtCodPostalMod.val('');
            }
        },

        InitDistritosSesion: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos3Type = {},
                parameters = {};

            objUbigeos3Type.strIdSession = Session.IDSESSION;
            objUbigeos3Type.dep = controls.cboDepMod.val();
            objUbigeos3Type.prov = controls.cboProvinciaMod.val();

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo3 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos3Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboDistritoMod.empty();
                            controls.cboDistritoMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (Ubigeo3 === value.Description) {
                                    controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboDistritoMod option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (idpais != 'PERU') {
                                $("#cboDistritoMod").prop("disabled", true);
                            } else {
                                $("#cboDistritoMod").prop("disabled", false);
                                $("#cboDistritoMod option:contains(" + iddist + ")").attr('selected', true);
                            }
                            that.InitPostalCodeSesion();

                        }
                    });
                }
            });
            that.getRulesUbigeoDist();
        },

        InitPostalCode: function (pid) {

            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objPostalCode = {};

            objPostalCode.variable = controls.cboDistritoMod.val();

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objPostalCode),
                url: '/IFITransactions/CommonServices/GetPostalCode',
                success: function (response) {
                    controls.txtCodPostalMod.val(response.data);
                }
            });
        },

        InitPostalCodeSesion: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }
            var that = this,
                controls = that.getControls(),
                objPostalCode = {};
            objPostalCode.variable = controls.cboDistritoMod.val();
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objPostalCode),
                url: '/IFITransactions/CommonServices/GetPostalCode',
                success: function (response) {
                    controls.txtCodPostalMod.val(response.data);

                }
            });

        },

        InitLastInvoiceData: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objLastInvoiceData = {},
                parameters = {};

            objLastInvoiceData.strIdSession = Session.IDSESSION;
            objLastInvoiceData.strCustomerCode = Session.CLIENTE.CustomerID;


            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo1 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objLastInvoiceData),
                        url: '/IFITransactions/CommonServices/GetLastInvoiceData',
                        success: function (response) {
                            if (response.data != null) { }
                            $.each(response.data.LastInvoiceDatas, function (index, value) {
                                fec = (response.data.LastInvoiceDatas[0].FECHA_VENCIMIENTO).substring(0, 10);
                                mto = "S/. " + response.data.LastInvoiceDatas[0].MTO_ULT_FACTURA;
                            });
                        }
                    });
                }
            });
        },

        CheckSiNo: function () {
            var that = this,
            controls = that.getControls();
            var chksn = document.getElementById("chkSN");
            var objhidCheck = document.getElementById('hidCheck');
            objhidCheck.value = 'SI';
            if (chksn.checked == true) {
                controls.cboTipoMz.prop('disabled', true);
                controls.cboTipoMz.prop('selectedIndex', 0);
                controls.txtNroMz.prop('disabled', true);
                controls.txtNroMz.val('');
                controls.txtLote.prop('disabled', true);
                controls.txtLote.val('');
                controls.txtNumeroCalle.val('S/N');
                that.ContadorD1();
            }
            else {
                controls.txtNumeroCalle.prop('disabled', false);
                controls.cboTipoMz.prop('disabled', false);
                controls.txtNroMz.prop('disabled', false);
                controls.txtNroMz.val('');
                controls.txtLote.prop('disabled', false);
                controls.txtLote.val('');
                controls.txtNumeroCalle.val('');
                that.ContadorD1();
            }
        },

        MostrarOcultarEmail: function () {
            var objCheck = document.getElementById('chkEmail');
            var objEmail = document.getElementById('txtEmail');
            if (objCheck.checked == true) {
                objEmail.style.display = "";
            }
            else {
                objEmail.style.display = "none";
            }
        },

        change_manzana: function () {
            var that = this,
            controls = that.getControls();

            document.getElementById('txtNroMz').value = '';
            document.getElementById('txtLote').value = '';

            if (document.getElementById('cboTipoMz').value == 'MZ') {
                document.getElementById('txtNumeroCalle').value = '';
                document.getElementById('txtNumeroCalle').disabled = true;
                document.getElementById('txtLote').disabled = false;
            }
            else {
                document.getElementById('txtNumeroCalle').disabled = false;
                document.getElementById('txtLote').value = '';
                document.getElementById('txtLote').disabled = true;
            }
        },

        valida_notas_direccion: function () {
            var objTipoUrbanizacion = document.getElementById('cboTipoUrb');
            var objNombreUrbanizacion = document.getElementById('txtUrb');
            var objTipoZona = document.getElementById('cboTipoZona');
            var objNombreEtapa = document.getElementById('txtZona');
            var objReferencias = document.getElementById('txtReferencia');
            var objTipoManzana = document.getElementById('cboTipoMz');
            var objLote = document.getElementById('txtLote');
            var ret = true;

            var strCad;

            if (objTipoUrbanizacion.selectedIndex > 0 && objNombreUrbanizacion.value == '') {
                ret = false;
                alert('Debe especificar el nombre de la urbanización');
                objNombreUrbanizacion.focus();
            }

            if (objTipoZona.selectedIndex > 0 && objNombreEtapa.value == '') {
                ret = false;
                alert('Debe especificar el nombre de la etapa');
                objNombreEtapa.focus();
            }
            if (objTipoManzana.value == 'MZ' && objLote.value == '') {
                ret = false;
                alert('Debe especificar el Lote');
                objLote.focus();
            }

            if (objReferencias.value == '') {
                ret = false;
                alert('Debe especificar las referencias de la dirección');
                objReferencias.focus();
            }

            strCad = '';
            if (objTipoUrbanizacion.selectedIndex > 0 && objNombreUrbanizacion.value != '') {
                strCad = strCad + objTipoUrbanizacion.options[objTipoUrbanizacion.selectedIndex].value;
                strCad = strCad + ' ' + objNombreUrbanizacion.value;
            }

            if (objTipoZona.selectedIndex > 0 && objNombreEtapa.value != '') {
                strCad = strCad + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
                strCad = strCad + ' ' + objNombreEtapa.value;
            }

            if (objReferencias.value != '') {
                if (strCad.length == 0) {
                    strCad = objReferencias.value;
                }
                else {
                    strCad = strCad + ' ' + objReferencias.value;
                }
            }


            if (ret) {
                var strNotas = new String(strCad);
                var intLon;
                intLon = strNotas.length;
                if (intLon > 40) {
                    ret = false;
                    alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                }
            }
            return ret;
        },

        valida_direccion: function () {
            var objTipoVia = document.getElementById('cboTipoVia');
            var objNombreCalle = document.getElementById('txtVia');
            var objNroCalle = document.getElementById('txtNumeroCalle');
            var objCheck = document.getElementById('chkSN');
            var objTipoMz = document.getElementById('cboTipoMz');
            var objManzana = document.getElementById('txtNroMz');
            var objLote = document.getElementById('txtLote');
            var objTipoInterior = document.getElementById('cboTipoInterior');
            var objNroInterior = document.getElementById('txtInterior');
            var objDireccion = document.getElementById('txtDireccion');
            var strDireccion = "";
            var ret = true;

            strCad = '';
            strCad = strCad + objTipoVia.options[objTipoVia.selectedIndex].value;
            strCad = strCad + ' ' + objNombreCalle.value;
            if (objNroCalle.value != '' && objNroCalle.disabled != true) {
                if (objNroCalle.value != 'S/N') {
                    strCad = strCad + ' ' + objNroCalle.value;
                }
            }
            if (objCheck.checked) {
                strCad = strCad + ' ' + 'S/N';
            }
            if (objTipoMz.value != 'Seleccionar') {
                strCad = strCad + ' ' + objTipoMz.value;
            }
            if (objManzana.value != '' && objManzana.disabled != true) {
                strCad = strCad + ' ' + objManzana.value;
            }
            if (objLote.value != '' && objLote.disabled != true) {
                strCad = strCad + ' LT ' + objLote.value;
            }
            if (objTipoInterior.selectedIndex > 0 && objNroInterior.value != '') {
                strCad = strCad + ' ' + objTipoInterior.options[objTipoInterior.selectedIndex].value;
                strCad = strCad + ' ' + objNroInterior.value;
            }

            var strDireccion = new String(strCad);
            intLon = strDireccion.length;
            if (intLon > 40) {
                ret = false;
                alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
            }
            objDireccion.value = strDireccion;
            return ret;

        },

        ContadorD1: function () {
            var objTipoVia = document.getElementById('cboTipoVia');
            var objNombreCalle = document.getElementById('txtVia');
            var objNroCalle = document.getElementById('txtNumeroCalle');
            var objCheck = document.getElementById('chkSN');
            var objTipoManzana = document.getElementById('cboTipoMz');
            var objManzana = document.getElementById('txtNroMz');
            var objLote = document.getElementById('txtLote');
            var objTipoInterior = document.getElementById('cboTipoInterior');
            var objNroInterior = document.getElementById('txtInterior');
            strCad = '';
            if (objTipoManzana.selectedIndex < 1) {
                objManzana.value = '';
                objManzana.disabled = true;
            } else {
                objManzana.disabled = false;
            }

            if (objTipoInterior.selectedIndex < 1) {
                objNroInterior.value = '';
                objNroInterior.disabled = true;
            } else {
                objNroInterior.disabled = false;
            }

            if (objTipoManzana.value == 'MZ') {
                objNroCalle.value = '';
                objNroCalle.disabled = true;
                objLote.disabled = false;
            }
            else {
                objNroCalle.disabled = false;
                objLote.value = '';
                objLote.disabled = true;
            }


            if (objTipoVia.selectedIndex > 0 && objTipoVia.value != '') {
                strCad = strCad + objTipoVia.options[objTipoVia.selectedIndex].value;
            }
            if (objNombreCalle.value != '') {
                strCad = strCad + ' ' + objNombreCalle.value;
            }
            if (objNroCalle.value != '' && objNroCalle.disabled != true) {
                if (objNroCalle.value != 'S/N')
                { strCad = strCad + ' ' + objNroCalle.value; }
            } else if (objNroCalle.disabled != false) {
                strCad = strCad
            }
            if (objCheck.checked) {
                strCad = strCad + ' ' + 'S/N';
                objNroCalle.disabled = true;
            }
            if (objTipoManzana.selectedIndex > 0 && objTipoManzana.value != '') {
                strCad = strCad + ' ' + objTipoManzana.options[objTipoManzana.selectedIndex].value;
            }
            if (objManzana.value != '' && objManzana.disabled != true) {
                strCad = strCad + ' ' + objManzana.value;
            }
            if (objLote.value != '' && objLote.disabled != true) {
                strCad = strCad + ' LT ' + objLote.value;
            }
            if (objTipoInterior.selectedIndex > 0 && objTipoInterior.value != '') {
                strCad = strCad + ' ' + objTipoInterior.options[objTipoInterior.selectedIndex].value;
            }
            if (objNroInterior.value != '' && objNroInterior.disabled != true) {
                strCad = strCad + ' ' + objNroInterior.value;
            }
            var strDireccion = new String(strCad);
            intLon = strDireccion.length;
            document.getElementById('hidContadorD1').value = intLon;
            document.getElementById('txtContadorD1').value = intLon;
        },

        ContadorD2: function () {
            var objTipoUrbanizacion = document.getElementById('cboTipoUrb');
            var objNombreUrbanizacion = document.getElementById('txtUrb');
            var objTipoZona = document.getElementById('cboTipoZona');
            var objNombreEtapa = document.getElementById('txtZona');
            var objReferencias = document.getElementById('txtReferencia');

            strCad = '';
            if (objTipoUrbanizacion.selectedIndex < 1) {
                objNombreUrbanizacion.value = '';
                objNombreUrbanizacion.disabled = true;
            } else {
                objNombreUrbanizacion.disabled = false;
            }
            if (objTipoZona.selectedIndex < 1) {
                objNombreEtapa.value = '';
                objNombreEtapa.disabled = true;
            } else {
                objNombreEtapa.disabled = false;
            }
            if (objTipoUrbanizacion.selectedIndex > 0 && objTipoUrbanizacion.value != '') {
                strCad = strCad + objTipoUrbanizacion.options[objTipoUrbanizacion.selectedIndex].value;
            }
            if (objNombreUrbanizacion.value != '' && objNombreUrbanizacion.disabled != true) {
                strCad = strCad + ' ' + objNombreUrbanizacion.value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex > 0 && objTipoZona.value != '') {
                strCad = strCad + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex == 0) {
                strCad = strCad + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objNombreEtapa.value != '' && objNombreEtapa.disabled != true) {
                strCad = strCad + ' ' + objNombreEtapa.value;
            }

            if (objReferencias.value != '' && (objTipoUrbanizacion.selectedIndex > 0 || objTipoZona.selectedIndex > 0)) {
                strCad = strCad + ' ' + objReferencias.value;
            }
            else {
                strCad = strCad + objReferencias.value;
            }
            var strNotas = new String(strCad);
            intLon = strNotas.length;
            document.getElementById('hidContadorD2').value = intLon;
            document.getElementById('txtContadorD2').value = intLon;
            strReferencia = strCad;
        },

        getRulesControls: function () {

            $.validator.addMethod(
                               "ValidateEmail",
                               function (value, element) {


                                   return $.IsEmail(value);

                               },
                               "* Ingrese un Correo Valido"
                                );
            $.validator.addMethod(
                    "ValidateDep",
                    function (value, element) {

                        return (value == "" || value == "Seleccionar") ? false : true;
                    },
                    "* Debe Seleccionar Departamento"
                     );

            $.validator.addMethod(
                "ValidateProv",
                function (value, element) {

                    return (value == "" || value == "Seleccionar") ? false : true;
                },
                "* Debe Seleccionar Provincia"
                 );

            $.validator.addMethod(
                "ValidateDist",
                function (value, element) {

                    return (value == "" || value == "Seleccionar") ? false : true;
                },
                "* Debe Seleccionar Distrito"
                 );

            $.validator.addMethod(
                    "ValidateCac",
                    function (value, element) {

                        return (value == "" || value == "Seleccionar") ? false : true;
                    },
                    "* Debe Seleccionar Punto de Atencion"
                     );

            if (strReferencia == "") {
                $('#frmChangePostalAddress').validate({
                    rules: {
                        nmtxtReferencia: {
                            required: true
                        }

                    }, highlight: function (element) {
                        $(element).closest('.error-input').addClass('has-error');
                    },
                    unhighlight: function (element) {
                        $(element).closest('.error-input').removeClass('has-error');
                    },
                    messages: {
                        nmtxtReferencia: {
                            required: "* El campo es Requerido",
                        }
                    }

                });
            }
            $('#frmChangePostalAddress').validate({
                rules: {
                    nmtxtEmail: {
                        required: true,
                        ValidateEmail: true
                    },
                    nmcboTipoVia: {
                        required: true
                    },
                    nmcboDepMod: {
                        ValidateDep: true,
                        required: true
                    },
                    nmcboProvinciaMod: {
                        ValidateProv: true,
                        required: true
                    },
                    nmcboDistritoMod: {
                        ValidateDist: true,
                        required: true
                    },
                    nmcboCacDac: {
                        ValidateCac: true,
                        required: true
                    },
                    nmtxtTipoVia: {
                        required: true
                    }

                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {

                    nmtxtEmail: {
                        required: "* El campo es Requerido",


                    },
                    nmcboTipoVia: {
                        required: "* La opción es Requerido",
                    },

                    nmtxtTipoVia: {
                        required: "* El campo es Requerido",
                    },
                    nmcboCacDac: {
                        required: "* Debe seleccionar centro de atención al cliente.",
                    },
                    nmcboDepMod: {
                        required: "*  Debe seleccionar departamento",
                    },
                    nmcboProvinciaMod: {
                        required: "* Debe seleccionar provincia.",
                    },
                    nmcboDistritoMod: {
                        required: "* Debe seleccionar distrito.",
                    }
                }

            });
        },

        getRulesUbigeoProv: function () {
            $('#frmChangePostalAddress').validate({
                rules: {
                    nmcboProvinciaMod: {
                        required: true
                    }
                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {
                    nmcboProvinciaMod: {
                        required: "* La opción es Requerida",
                    }
                }

            });
        },

        getRulesUbigeoDist: function () {
            $('#frmChangePostalAddress').validate({
                rules: {
                    nmcboDistritoMod: {
                        required: true
                    }
                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {
                    nmcboDistritoMod: {
                        required: "* La opción es Requerida",
                    }
                }

            });
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

        validarMaxCaracteres: function () {

            nombre = $('#txtContadorD1').val();
            if (nombre < 41) {
                return true;
            }
            else {
                alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMaxCaracteres2: function () {
            nombre2 = $('#txtContadorD2').val();
            if (nombre2 < 41) {
                return true;
            }
            else {
                alert('Las notas de la dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMinCaracteres2: function () {
            nombre2 = $('#txtContadorD2').val();
            if (nombre2 == '') {
                alert('Debe ingresar notas de la dirección');
                return false;
            }
            if (nombre2 == 0) {
                alert('Debe ingresar notas de la dirección');
                return false;
            }
            return true;
        },

        validarMaxCaracteresNote: function () {

            nombre1 = $('#txtNote').val().length;
            if (nombre1 < 3800) {
                return true;
            }
            else {
                alert('El maximo de caracteres permitidos en el campo notas es de 3800');
                return false;
            }
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

    $.fn.INTChangePostalAddress = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTChangePostalAddress'),
                options = $.extend({}, $.fn.INTChangePostalAddress.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTChangePostalAddress', data);
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

    $.fn.INTChangePostalAddress.defaults = {
    }

    $('#divBody').INTChangePostalAddress();
})(jQuery);