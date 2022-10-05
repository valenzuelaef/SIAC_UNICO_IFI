var mto, fec, intLon, strDireccion, strReferencia, Enviomail, idpais, iddep, idprov, iddist, genOCC, cober, fideliza;
var SessionCSA = {};
var SessionFlag = 0;
var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};

function f_cancelAuth()
{
    //$("#pnlFidelizar").css("display", "run-in");
    $("#chkFidelizar").prop("checked", false);
    //$("#rdbSi").prop("checked", false);
    //$("#btnGuardar").prop("disabled", true);
    genOCC = "1";
}

function CloseValidation(obj, pag, controls)
{
    var mensaje;
    if (obj.hidAccion === 'G') {// Correcto
        genOCC = "0";
        $("#txtImporte").val("S/. 0.00");
        $("#chkFidelizar").prop("disabled", true);
    } else {
        genOCC = "1";
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");
        $("#chkFidelizar").prop("disabled", false);
        return;
    }
}

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
        $.extend(this, $.fn.INTChangeServiceAddress.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            //Titulo
            , lblTitle: $('#lblTitle', $element)
            , cboCacDac: $("#cboCacDac", $element)
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
            //Nacionalidad
            , cboPaisMod: $("#cboPaisMod", $element)
            , cboDepMod: $("#cboDepMod", $element)
            , cboProvinciaMod: $("#cboProvinciaMod", $element)
            , cboDistritoMod: $("#cboDistritoMod", $element)
            , txtCodPostalMod: $("#txtCodPostalMod", $element)
            //textbox
            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnCobertura: $("#btnCobertura", $element)
            , myModalLoad: $("#myModalLoad", $element)
            , lblErrorMessage: $("#lblErrorMessage", $element)
            , divReglas: $("#divReglas", $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , cboTipoVia: $("#cboTipoVia", $element)
            , txtNomVia: $("#txtNomVia", $element)
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
            , txtTelefonoReferencia: $("#txtTelefonoReferencia", $element)
            , rdbSi: $("#rdbSi", $element)
            , rdbNo: $("#rdbNo", $element)
            , pnlFidelizar: $("#pnlFidelizar", $element)
            , txtImporte: $("#txtImporte", $element)
            , chkFidelizar: $("#chkFidelizar", $element)
            , txtNote: $('#txtNote', $element)
            , divCobertura: $("#divCobertura", $element)
            , hidMonto: $("#hidMonto", $element)
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();
            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_click);
            controls.btnCobertura.addEvent(that, 'click', that.btnCobertura_click);
            controls.cboPaisMod.addEvent(that, 'change', that.InitValidaPais);
            controls.cboDepMod.addEvent(that, 'change', that.InitProvincias);
            controls.cboProvinciaMod.addEvent(that, 'change', that.InitDistritos);
            controls.cboDistritoMod.addEvent(that, 'change', that.InitPostalCode);
            controls.cboTipoVia.addEvent(that, 'change', that.ContadorD1);
            controls.txtNomVia.addEvent(that, 'change', that.ContadorD1);
            controls.txtNumeroCalle.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoMz.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoMz.addEvent(that, 'change', that.ContadorD1);
            controls.txtNroMz.addEvent(that, 'change', that.ContadorD1);
            controls.txtLote.addEvent(that, 'change', that.ContadorD1);
            controls.chkSN.addEvent(that, 'change', that.CheckSiNo);
            controls.cboTipoInterior.addEvent(that, 'change', that.ContadorD1);
            controls.txtInterior.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoUrb.addEvent(that, 'change', that.ContadorD1);
            controls.txtUrb.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoZona.addEvent(that, 'change', that.ContadorD1);
            controls.txtZona.addEvent(that, 'change', that.ContadorD1);
            controls.txtReferencia.addEvent(that, 'change', that.ContadorD1);
            controls.rdbSi.addEvent(that, 'change', that.rdbSi_Click);
            controls.rdbNo.addEvent(that, 'change', that.rdbNo_Click);
            controls.chkFidelizar.addEvent(that, "change", that.chkFidelizar_Click);

            SessionCSA.strCostChangeServiceAddress = strCostChangeServiceAddress;
            SessionCSA.strErrChangeServiceAddress = strErrChangeServiceAddress;
            SessionCSA.strViewChangeServiceAddress = strViewChangeServiceAddress;
            SessionCSA.strSaveChangeServiceAddress = strSaveChangeServiceAddress;
            SessionCSA.strTotalChangeServiceAddress = strTotalChangeServiceAddress;
            SessionCSA.strCobertChangeServiceAddress = strCobertChangeServiceAddress;
            that.InitFlagGeneral();
            that.InitBlockGeo();
            that.maximizarWindow();
            that.render();
                        
            alert(SessionCSA.strCostChangeServiceAddress, 'Alerta');
            
            if ((SessionCSA.strTotalChangeServiceAddress !== null) || (SessionCSA.strTotalChangeServiceAddress !== undefined)) {
                controls.hidMonto.val(SessionCSA.strTotalChangeServiceAddress);
            } else {
                controls.hidMonto.val("");
            }
        },

        modelChangeServiceAddress: {},
        modelDireccion: {},
        modelCliente: {},
        modelSolicitud:{},
        modelValidarCobertura: {},

        render: function () {
            var that = this,
            control = that.getControls();
            control.pnlFidelizar.hide();
          
            control.btnGuardar.prop('disabled', true);
            control.divErrorAlert.hide();
            control.divErrorAlert.hide();
            that.Loading();
            
            that.loadCustomerData();
            that.InitVias();
            that.InitManzanas();
            that.InitInteriores();
            that.InitUrbs();
            that.InitZonas();
            that.InitCacDaCSesion();
            that.IniTypification();
            that.getRulesControls();
            
            if (SessionFlag == 1) {
                control.divCobertura.hide();
            }
        },

        IniTypification: function () {
            var obj = { strIdSession: Session.IDSESSION };
            $.app.ajax({
                type: 'POST',
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangeServiceAddress/PageLoad',
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

        IniBegin: function () {
            var that = this,
            controls = this.getControls();

            that.IniLoadPage();
        },

        InitBlockGeo: function () {
            var that = this;
            var controls = this.getControls();
            console.log(SessionFlag)
            if (SessionFlag == 1) {
               
            } else {
                controls.txtNomVia.prop('disabled', false);
                controls.txtUrb.prop('disabled', false);
                controls.txtZona.prop('disabled', false);
            }
        },

        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();
            controls.lblTitle.text("CAMBIO DE DIRECCIÓN DE USO DEL SERVICIO IFI");
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
            idpais = Session.CLIENTE.InvoiceCountry.toUpperCase();
            genOCC = "1";
            
            that.InitNacionalidad();

            $("#txtNomVia").keypress(function (e) {
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
            $("#txtTelefonoReferencia").on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });
        },

        InitFlagGeneral: function () {
            var that = this,
                controls = this.getControls();
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(Session.IDSESSION),
                url: '/IFITransactions/ChangeServiceAddress/obtenerFlagCobertura',
                error: function (data) {
                    alert(SessionCSA.strErrChangeServiceAddress, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnCobertura").attr('disabled', true);
                },
                success: function (response) {
                    if (response != null)
                    {
                        if (response.data != null)
                        {
                            SessionFlag = response.data;
                            if (response.data != 0) {
                                controls.divCobertura.hide();
                                controls.btnGuardar.prop('disabled', true);
                                controls.cboPaisMod.prop('disabled', true);
                                controls.cboDepMod.prop('disabled', true);
                                controls.cboProvinciaMod.prop('disabled', true);
                                controls.cboDistritoMod.prop('disabled', true);
                                controls.cboTipoVia.prop('disabled', true);
                                controls.txtNomVia.prop('disabled', true);
                                controls.txtNumeroCalle.prop('disabled', true);
                                controls.cboTipoMz.prop('disabled', true);
                                controls.cboTipoMz.prop('disabled', true);
                                controls.txtNroMz.prop('disabled', true);
                                controls.txtLote.prop('disabled', true);
                                controls.chkSN.prop('disabled', true);
                                controls.cboTipoInterior.prop('disabled', true);
                                controls.txtInterior.prop('disabled', true);
                                controls.cboTipoUrb.prop('disabled', true);
                                controls.txtUrb.prop('disabled', true);
                                controls.cboTipoZona.prop('disabled', true);
                                controls.txtZona.prop('disabled', true);
                                controls.rdbSi.prop('disabled', true);
                                controls.rdbNo.prop('disabled', true);
                                controls.chkFidelizar.prop('disabled', true);
                                controls.txtNote.prop('disabled', true);
                                $("#rdbSi").prop("disabled", true);
                                $("#rdbNo").prop("disabled", true);
                            } else {
                                controls.divCobertura.show();
                                controls.txtNomVia.prop('disabled', false);
                                controls.txtUrb.prop('disabled', false);
                                controls.txtZona.prop('disabled', false);
                            }
                        }
                    }
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

        valida_direccion: function () {
            var that = this,
                    controls = this.getControls();
            var ret = true;
            if ((controls.cboTipoUrb.val() == '') &&
                (controls.cboTipoZona.val() == '') &&
                (controls.txtReferencia.val() == '')) {
                alert('Se debe de seleccionar Tipo de Urbanización/Tipo de Zona e ingresar referencia.', 'Validación');
                return false;
            }

            if ((controls.cboTipoUrb.val() != '' && controls.txtUrb.val() == '') &&
                (controls.cboTipoZona.val() == '' && controls.txtZona.val() == '' )) {
                alert('Ingresar el nombre del tipo de urbanizacion.', 'Validación');
                return false;
            }

            if ((controls.cboTipoUrb.val() == '' && controls.txtUrb.val() == '') &&
                (controls.cboTipoZona.val() != '' && controls.txtZona.val() == '')) {
                alert('Ingresar el nombre del tipo de zona.', 'Validación');
                return false;
            }

            if ((controls.cboTipoUrb.val() == '' && controls.txtUrb.val() == '') &&
                (controls.cboTipoZona.val() == '' && controls.txtZona.val() == '') &&
                (controls.txtReferencia.val() == '')) {
                alert('Ingresar una referencia válida.', 'Validación');
                return false;
            }
            return ret;
        },

        btnGuardar_click: function () {
            if ($('#frmChangeServiceAddress').valid()) {
                var that = this,
                    controls = this.getControls();

                if (!that.valida_direccion()) {
                    return false;
                }

                confirm("¿Seguro que desea continuar?", 'Confirmar', function () {
                    that.Loading();
                    that.SaveTransactionChangeService();

                }, function () {
                    $("#hidAccion").val("");
                    return false;
                });
            }
        },
        
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',

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
                },
                timeout: 3000
            });
        },

        rdbSi_Click: function () {
            var that = this,
                controls = that.getControls();
            controls.btnGuardar.prop('disabled', false);
            controls.pnlFidelizar.show();
            cober = "1";
            if ((SessionCSA.strTotalChangeServiceAddress !== null) || (SessionCSA.strTotalChangeServiceAddress !== undefined)) {
                controls.txtImporte.val("S/. " + SessionCSA.strTotalChangeServiceAddress);
            } else {
                controls.txtImporte.val("");
            }
        },

        rdbNo_Click: function () {
            var that = this,
                controls = that.getControls();
            controls.pnlFidelizar.hide();
            controls.btnGuardar.prop('disabled', false);
            controls.chkFidelizar.prop("checked", false);
            controls.chkFidelizar.prop("disabled", false);
            //controls.btnCobertura.prop('disabled', true);
            controls.btnGuardar.prop('disabled', false);
            //controls.txtImporte.val("");
            cober = "0";
            alert(SessionCSA.strViewChangeServiceAddress,'Alerta');
        },

        chkFidelizar_Click: function () {
            var that = this,
                       controls = that.getControls();
            var option = 'gConstkeyValidaFidelizar';
            //var co = '500';
            //var param = {
            //    "strIdSession": Session.IDSESSION,
            //    'pag': '1',
            //    'opcion': 0,
            //    'co': co
            //};
            
            //ValidateAccess(that, controls, 'IMP', 'gConstEvtFechaAplicacionCP', '1', param, 'Postpaid');
            $.window.open({
                autoSize: false,
                controlBox: false,
                url: that.getUrl + '/IFITransactions/Auth/AuthUserHtml',
                type: 'POST',
                title: 'SIACUNICO - Autenticación',
                modal: true,
                width: 360,
                height: 310,
                buttons: {
                    Validar: {
                        class: 'btn transaction-button btn-sm',
                        click: function (sender, args) {
                            var usu = $('#txtUsernameAuth').val();
                            var pass = $('#txtPasswordAuth').val();
                            if (usu === "") {
                                alert('Debe ingresar un nombre de usuario.', "Alerta");
                                return;
                            }

                            if (pass === "") {
                                alert('Debe ingresar una contraseña.', "Alerta");
                                return;
                            }
                            
                            var $this = this;
                            $.app.ajax({
                                type: "POST",
                                cache: false,
                                dataType: "json",
                                url: '/IFITransactions/CommonServices/CheckingUser',
                                data: { strIdSession: Session.IDSESSION, user: usu, pass: pass, option: option },
                                error: function (ex) {
                                    if (ex != null) {
                                        genOCC = "1";
                                        modalAlert('Ocurrio un error al Validar el Usuario.', 'Notificación');
                                    }
                                },
                                beforeSend: function () {
                                    $.blockUI({
                                        message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
                                complete: function () {
                                    $.unblockUI();
                                },
                                success: function (response) {
                                    if (response.result && response.result == 1) {
                                        genOCC = "0";
                                        $("#txtImporte").val("S/. 0.00");
                                        $("#chkFidelizar").prop("disabled", true);
                                        $this.close();
                                    } else if (response.result == 2 || response.result == 0) {
                                        genOCC = "1";
                                        modalAlert('La validacion del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.');
                                        $("#chkFidelizar").prop("disabled", false);
                                    } else if (response.result == 3) {
                                        genOCC = "1";
                                        modalAlert('Ocurrio un error al Validar el Usuario.');
                                        $("#chkFidelizar").prop("disabled", false);
                                    }
                                }
                            });
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            $("#chkFidelizar").prop("checked", false);
                            genOCC = "1";
                            this.close();
                        }
                    }
                }
            });
        },

        btnCobertura_click: function () {
            var that = this,
                control = that.getControls();
            var url = SessionCSA.strCobertChangeServiceAddress;
            var bExisteCobertura = false;

            if (SessionFlag == 1) {
                that.modelValidarCobertura.idTransaccion = Session.IDSESSION;
                that.modelValidarCobertura.strDepartamento = $('#cboDepMod option:selected').text();
                that.modelValidarCobertura.strProvincia = $('#cboProvinciaMod option:selected').text();
                that.modelValidarCobertura.strDistrito = $('#cboDistritoMod option:selected').text();

                that.modelChangeServiceAddress.strTmCode = Session.LINEA.CodePlanTariff;
                that.modelChangeServiceAddress.idTransaccion = Session.IDSESSION;
                that.modelChangeServiceAddress.codAplicacion = "TV";
                that.modelChangeServiceAddress.tipoTecnologia = "IFI_5G";
                that.modelChangeServiceAddress.motivo = "";
                that.modelCliente.tipoDoc = "1";
                that.modelCliente.numeroDoc = Session.CLIENTE.DNIRUC;
                that.modelCliente.nombres = Session.CLIENTE.LastName;
                that.modelCliente.apellidos = Session.CLIENTE.Name;
                that.modelCliente.correo = Session.CLIENTE.Email;
                that.modelCliente.telefonoContacto = Session.CLIENTE.Telephone;
                that.modelSolicitud = null;

                $.window.open({
                    url: that.getUrl + '/IFITransactions/ChangeServiceAddress/ChangeServiceGeoDir',
                    title: 'Buscar Dirección de uso del equipo',
                    modal: true,
                    width: 890,
                    height: 680,
                    maximizeBox: false,
                    minimizeBox: false,
                    buttons: {

                        Validar: {
                            "class": 'btn transaction-button btn-sm',
                            id: 'btnValidarCobertura',
                            click: function (sender, args) {
                                //Información adicional
                                that.modelChangeServiceAddress.latitud = Session.DatosDireccionMapaLat;
                                that.modelChangeServiceAddress.longitud = Session.DatosDireccionMapaLon;

                                //Dirección de ubicación actual seleccionada
                                that.modelDireccion.departamento = Session.DatosDireccionMapaDepartamento;
                                that.modelDireccion.provincia = Session.DatosDireccionMapaProvincia;
                                that.modelDireccion.distrito = Session.DatosDireccionMapaDistrito;
                                that.modelDireccion.direccion = Session.DatosDireccionMapaDirStd;

                                var objCobertura = {
                                    oChangeServiceAdd: that.modelChangeServiceAddress,
                                    oServicio: that.modelChangeServiceAddress,
                                    oCliente: that.modelCliente,
                                    oDireccion: that.modelDireccion,
                                    oSolicitud: that.modelSolicitud
                                };

                                $.ajax({
                                    type: 'POST',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: 'json',
                                    data: JSON.stringify(objCobertura),
                                    url: '/IFITransactions/ChangeServiceAddress/consultarCoberturaDireccion',
                                    type: 'post',
                                    success: function (response) {
                                        if (response.data != null) {
                                            if (response.data.exiteCobertura == true) {
                                                $("#divExito").show(); $("#divError").hide();
                                                $("#rdbSi").prop("checked", true);
                                                $("#rdbNo").prop("checked", false);
                                                that.rdbSi_Click();
                                                control.chkFidelizar.prop('disabled', false);
                                                bExisteCobertura = true;
                                            } else {
                                                $("#divError").show(); $("#divExito").hide();
                                                $("#rdbSi").prop("checked", false);
                                                $("#rdbNo").prop("checked", true);
                                                
                                                if ((response.data.Mensaje.indexOf('null') > 0) || (response.data.Mensaje !== '')){
                                                    alert(response.data.Mensaje);
                                                }
                                            }
                                            $("#btnCobertura").attr("disabled", true);
                                            $("#btnValidarCobertura").attr("disabled", true);
                                        }
                                    },
                                    error: function (data) {
                                        alert(SessionCSA.strErrChangeServiceAddress, "Alerta");
                                        $("#btnGuardar").attr('disabled', true);
                                        $("#btnCobertura").attr('disabled', true);
                                    }
                                });
                            }
                        },
                        Cerrar: {
                            click: function (sender, args) {
                                that.getLoadInformation();
                              //  if ((Session.DatosDireccionMapaTipoVia == '') && (Session.DatosDireccionMapaNombreVia == ''))
                              //  {
                                    //$("#rdbSi").prop("checked", false);
                                    //$("#rdbNo").prop("checked", true);
                                    //$("#btnCobertura").attr("disabled", true);
                                    //that.rdbNo_Click();
                               // } else {
                                if (bExisteCobertura == false) {
$("#rdbSi").prop("checked", false);
                                        $("#rdbNo").prop("checked", true);
                                        $("#btnCobertura").attr("disabled", true);
                                    that.rdbNo_Click();
                                }
                               // }
                                this.close();
                            }
                        }
                    }
                });
            } else {
                window.open(url, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=778, height=640');
            control.divCobertura.show();
            }
        },

        getLoadInformation: function () {
            var that = this,
                controls = that.getControls();

            controls.divCobertura.show();

            that.Loading();

            var normalize = (function () {
                var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
                    to = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuunncc",
                    mapping = {};

                for (var i = 0, j = from.length; i < j; i++)
                    mapping[from.charAt(i)] = to.charAt(i);

                return function (str) {
                    var ret = [];
                    for (var i = 0, j = str.length; i < j; i++) {
                        var c = str.charAt(i);
                        if (mapping.hasOwnProperty(str.charAt(i)))
                            ret.push(mapping[c]);
                        else
                            ret.push(c);
                    }
                    return ret.join('');
                }
            
            })();

            if ((Session.DatosDireccionMapaTipoVia != null) || (Session.DatosDireccionMapaTipoVia != '')) {
                $("#cboTipoVia option").filter(function () {
                    return $(this).text().toUpperCase().indexOf(normalize(Session.DatosDireccionMapaTipoVia)) > -1;
                }).prop('selected', true);
            }

            $("#cboTipoVia").attr('disabled', $("#cboTipoVia").val() == '' ? false : true);
            if ($("#cboTipoVia").val() == '' || $("#cboTipoVia").val() == 'ZZ') {
                controls.cboTipoVia.prop("disabled", false);
            }

            controls.txtNomVia.val(Session.DatosDireccionMapaNombreVia);
            controls.txtNomVia.prop('disabled', controls.txtNomVia.val() == '' ? false : true);

            controls.txtNumeroCalle.val(Session.DatosDireccionMapaNroVia);
            controls.txtNumeroCalle.prop('disabled', controls.txtNumeroCalle.val() == '' ? false : true);

            $("#cboTipoMz option").filter(function () {
                return $(this).text() == Session.DatosDireccionMapaSublocalidad;
            }).prop('selected', true);


            $("#cboTipoUrb").attr('disabled', $("#cboTipoUrb").val() == '' ? false : true);


            if ($("#cboTipoMz").val() == '') {
                $("#cboTipoMz").attr('disabled', $("#cboTipoMz").val() == '' ? false : true);
            }

            controls.txtUrb.prop("disabled", $("#cboTipoMz").val() == '' ? false : true);

            controls.cboTipoMz.prop("disabled", false);
            controls.txtNroMz.prop("disabled", false);
            controls.txtLote.prop("disabled", false);

            controls.cboTipoInterior.prop("disabled", false);
            controls.txtInterior.prop("disabled", false);

            controls.cboTipoZona.prop("disabled", false);
            controls.txtZona.prop("disabled", false);
            
            controls.cboDepMod.prop("disabled", true);
            controls.cboProvinciaMod.prop('disabled', true);
            controls.cboDistritoMod.prop('disabled', true);

            that.InitDepartamentos();

            that.InitProvincias();

            that.InitDistritos();

            controls.txtNote.prop("disabled", false);
            $("#txtUrb").val(Session.DatosDireccionMapaSublocalidad);

            if ($("#cboTipoUrb").val() == '') {
                $("#cboTipoUrb").prop("disabled", false);
                if ($("#txtUrb").val() != '') {
                    $("#txtUrb").prop("disabled", true)
                    $("#cboTipoUrb").prop("disabled", false);
                } else {
                    $("#txtUrb").prop("disabled", false);
                    $("#cboTipoUrb").prop("disabled", false);
                }
            } else {
                if ((Session.DatosDireccionMapaTipoSublocalidad != null) || (Session.DatosDireccionMapaTipoSublocalidad != '')) {
                    $("#cboTipoUrb option").filter(function () {
                        return $(this).text().toUpperCase().indexOf(normalize(Session.DatosDireccionMapaTipoSublocalidad)) > -1;
                    }).prop('selected', true);
                }
            }
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
            controls = that.getControls();
            if (SessionFlag == 1) {
                controls.cboTipoVia.prop("disabled", true);
                controls.txtNomVia.prop("disabled", true);
                controls.txtNumeroCalle.prop("disabled", true);
                controls.cboTipoMz.prop("disabled", true);
                controls.txtNroMz.prop("disabled", true);
                controls.txtLote.prop("disabled", true);
                controls.cboTipoInterior.prop("disabled", true);
                controls.txtInterior.prop("disabled", true);
                controls.cboTipoUrb.prop("disabled", true);
                controls.txtUrb.prop("disabled", true);
                controls.cboTipoZona.prop("disabled", true);
                controls.txtZona.prop("disabled", true);
                controls.txtReferencia.prop("disabled", true);
                controls.txtTelefonoReferencia.prop("disabled", true);
                controls.txtNote.prop('disabled', true);
                controls.chkFidelizar.prop('disabled', true);
                controls.cboDepMod.prop('disabled', true);
                controls.cboProvinciaMod.prop('disabled', true);
                controls.cboDistritoMod.prop('disabled', true);
                controls.cboCacDac.prop('disabled', true);
            } else {
                controls.txtNote.prop('disabled', true);
            }
        },

        SaveTransactionChangeService: function () {

            var that = this,
                controls = that.getControls();
            
            that.modelChangeServiceAddress.strIdSession = Session.IDSESSION;
            that.modelChangeServiceAddress.idTransaccion = Session.IDSESSION;
            that.modelChangeServiceAddress.strCustomerId = Session.CLIENTE.CustomerID;
            that.modelChangeServiceAddress.linea = Session.LINEA.CellPhone;
            
            //Direccion
            that.modelChangeServiceAddress.tipoDireccion = $("#cboTipoVia option:selected").text();
            that.modelChangeServiceAddress.nombreDireccion = $("#txtNomVia").val();
            that.modelChangeServiceAddress.numeroDireccion = $("#txtNumeroCalle").val();
            //SubDireccion
            that.modelChangeServiceAddress.subDireccion = $("#cboTipoMz option:selected").val();
            that.modelChangeServiceAddress.nombreSubDireccion = $("#cboTipoMz option:selected").text();

            that.modelChangeServiceAddress.tipoSubDireccion = $("#cboTipoInterior option:selected").text();
            that.modelChangeServiceAddress.numeroSubDireccion = $("#txtInterior").val();

            that.modelChangeServiceAddress.strDireccion = strDireccion.toUpperCase();

            that.modelChangeServiceAddress.lote = $("#txtLote").val();

            that.modelChangeServiceAddress.tipoUrbanizacion = $("#cboTipoUrb option:selected").text();
            that.modelChangeServiceAddress.nombreUrbanizacion = $("#txtUrb").val();
            that.modelChangeServiceAddress.zonaEtapa = $("#cboTipoZona option:selected").text();
            that.modelChangeServiceAddress.nombreZonaEtapa = $("#txtZona").val();
            that.modelChangeServiceAddress.referencia = ($("#txtReferencia").val()).toUpperCase();
            that.modelChangeServiceAddress.fideliza = $("#chkFidelizar").val();
            that.modelChangeServiceAddress.strNote = $("#txtNote").val();
            that.modelChangeServiceAddress.cobertura = cober;

            //Nacionalidad
            that.modelChangeServiceAddress.pais = $('#cboPaisMod option:selected').text();
            that.modelChangeServiceAddress.departamento = $('#cboDepMod option:selected').text();
            that.modelChangeServiceAddress.provincia = $('#cboProvinciaMod option:selected').text();
            that.modelChangeServiceAddress.distrito = $('#cboDistritoMod option:selected').text();
            that.modelChangeServiceAddress.codigoPostal = $('#txtCodPostalMod').val();
            that.modelChangeServiceAddress.telefonoReferencia = controls.txtTelefonoReferencia.val();
            that.modelChangeServiceAddress.ubigeo = genOCC;
            that.modelChangeServiceAddress.flagCobro = genOCC;
            that.modelChangeServiceAddress.monto = $("#hidMonto").val();
            that.modelChangeServiceAddress.strObjidContacto = Session.CLIENTE.IdContactObject;
            that.modelChangeServiceAddress.strNombres = Session.CLIENTE.Nombres;
            that.modelChangeServiceAddress.strApellidos = Session.CLIENTE.Apellidos;
            that.modelChangeServiceAddress.strTelefono = Session.CLIENTE.Telephone;

            that.modelChangeServiceAddress.account = Session.CLIENTE.Account;
            that.modelChangeServiceAddress.contractId = Session.CLIENTE.ContractID;
            that.modelChangeServiceAddress.currentUser = Session.ACCESO.login;
            that.modelChangeServiceAddress.customerId = Session.CLIENTE.CustomerID;
            that.modelChangeServiceAddress.tipo = TYPIFICATION.Tipo;
            that.modelChangeServiceAddress.claseDes = TYPIFICATION.ClaseDes;
            that.modelChangeServiceAddress.subClaseDes = TYPIFICATION.SubClaseDes;
            that.modelChangeServiceAddress.claseCode = TYPIFICATION.ClaseId;
            that.modelChangeServiceAddress.subClaseCode = TYPIFICATION.SubClaseId;
            that.modelChangeServiceAddress.tipoCode = TYPIFICATION.TipoId;
            //extras prueba constancia
            //that.modelChangeServiceAddress.strfullNameUser = Session.ACCESO.fullName;
            that.modelChangeServiceAddress.strNombres = Session.CLIENTE.Name;
            that.modelChangeServiceAddress.strApellidos = Session.CLIENTE.FullName;
            that.modelChangeServiceAddress.strfullNameUser = Session.CLIENTE.LegalAgent;
            that.modelChangeServiceAddress.DNI_RUC = Session.CLIENTE.DocumentNumber;
            that.modelChangeServiceAddress.strTipoDocumento = Session.CLIENTE.DocumentType;
            that.modelChangeServiceAddress.strCacDac = $('#cboCacDac option:selected').text();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelChangeServiceAddress),
                url: '/IFITransactions/ChangeServiceAddress/SaveTransactionChangeServiceAddress',
                error: function (data) {
                    alert(SessionCSA.strErrChangeServiceAddress, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnCobertura").attr('disabled', true);
                },
                success: function (response) {
                    if (response !== null)
                    {
                        if (response.data == "NO OK") {
                            if (response.msj == "") {
                            alert(SessionCSA.strErrChangeServiceAddress, "Alerta");
                                controls.btnGuardar.prop('disabled', true);
                                controls.rdbSi.prop('disabled', true);
                                controls.rdbNo.prop('disabled', true);
                                $("#chkFidelizar").prop("disabled", true);
                        } else {
                                alert(response.msj, "Alerta");
                                controls.btnGuardar.prop('disabled', true);
                                controls.rdbSi.prop('disabled', true);
                                controls.rdbNo.prop('disabled', true);
                            }
                        } else {
                            if (response.msj == "") {
                            alert(SessionCSA.strSaveChangeServiceAddress, "Informativo");
                            controls.btnGuardar.prop('disabled', true);
                            controls.rdbSi.prop('disabled', true);
                            controls.rdbNo.prop('disabled', true);
                            } else {
                                alert(response.msj, "Informativo");
                                controls.btnGuardar.prop('disabled', true);
                                controls.rdbSi.prop('disabled', true);
                                controls.rdbNo.prop('disabled', true);
                            }
                        }
                        that.BlockControl();
                    }
                }
            });
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
                            that.InitDepartamentos();
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

                            if ($("#cboTipoVia").val() == '' || $("#cboTipoVia").val() == 'ZZ') {
                                controls.cboTipoVia.prop("disabled", false);
                            }
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
                            return false;
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

        InitDistritos: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos3Type = {},
                parameters = {};
                            var itemSelect;

            objUbigeos3Type.prov = controls.cboProvinciaMod.val();
            if (objUbigeos3Type.prov == '') {
                controls.cboDistritoMod.empty();
                controls.cboDistritoMod.prop('disabled', true);
                controls.txtCodPostalMod.val('');
                                } else {
                objUbigeos3Type.strIdSession = Session.IDSESSION;

                if (SessionFlag == 1) {
                    objUbigeos3Type.dep = $("#cboDepMod").val();
                    objUbigeos3Type.prov = $("#cboProvinciaMod").val();
                } else {
                    objUbigeos3Type.dep = controls.cboDepMod.val();
                    objUbigeos3Type.prov = controls.cboProvinciaMod.val();
                }

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

                                $.each(response.data.UbigeoTypes, function (index, value) {
                                    if (SessionFlag == 1) {
                                        if (Session.DatosDireccionMapaDistrito === value.Description) {
                                            controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                            controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                        }
                                    } else {
                                        controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                    $("#cboDistritoMod option[value=" + itemSelect + "]").attr("selected", true);
                            }
                                if (SessionFlag == 1) {
                                    controls.cboDistritoMod.prop('disabled', true);
                                if ($("#cboDistritoMod").val() == '') {
                                    controls.cboDistritoMod.prop('disabled', false);
                                }
                            } else {
                                    controls.cboDistritoMod.prop('disabled', false);
                            }
                                that.InitPostalCode();
                        }
                    });
                }
            });
                that.getRulesUbigeoDist();
            }
        },

        InitProvincias: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }
            var that = this,
                controls = that.getControls(),
                objUbigeos2Type = {},
                parameters = {};
            var itemSelect;
            objUbigeos2Type.dep = controls.cboDepMod.val();
            
            if (objUbigeos2Type.dep == '') {
                controls.cboProvinciaMod.empty();
                controls.cboDistritoMod.empty();
                controls.cboProvinciaMod.prop('disabled', true);
                controls.cboDistritoMod.prop('disabled', true);
                controls.txtCodPostalMod.val('');
            } else {

                objUbigeos2Type.strIdSession = Session.IDSESSION;
               
                if (SessionFlag == 1) {
                    objUbigeos2Type.dep = $("#cboDepMod").val();
                } else {
                objUbigeos2Type.dep = controls.cboDepMod.val();
                }
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
                                $.each(response.data.UbigeoTypes, function (index, value) {
                                    if (SessionFlag == 1) {
                                        if (Session.DatosDireccionMapaProvincia === value.Description) {
                                        controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                        itemSelect = value.Code;

                                    } else {
                                        controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    }
                                } else {
                                    controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboProvinciaMod option[value=" + itemSelect + "]").attr("selected", true);
                            }
                                if (SessionFlag == 1) {
                                    controls.cboProvinciaMod.prop('disabled', true);
                                    if ($("#cboProvinciaMod").val() == '') {
                                        controls.cboProvinciaMod.prop('disabled', false);
                                    }

                                    that.InitDistritos();
                            } else {
                                    controls.cboProvinciaMod.prop('disabled', false);
                            }
                controls.cboDistritoMod.prop('disabled', true);
                controls.txtCodPostalMod.val('');
                            }
                        });
                    }
                });
                that.getRulesUbigeoProv();
                that.getRulesUbigeoDist();
            }
        },

        InitDepartamentos: function () {
            var that = this,
                controls = that.getControls(),
                objUbigeos1Type = {},
                parameters = {};
            var itemSelect;
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

                            $.each(response.data.UbigeoTypes, function (index, value) {
                                if (SessionFlag == 1) {
                                    if (Session.DatosDireccionMapaDepartamento === value.Description) {
                                        controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;
                                } else {
                                        controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    }
                                } else {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            controls.cboProvinciaMod.prop('disabled', true);
                            controls.cboDistritoMod.prop('disabled', true);
                            if (SessionFlag == 1) {
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                    $("#cboDepMod option[value=" + itemSelect + "]").attr("selected", true);
                                } else if (Session.DatosDireccionMapaDepartamento === "CALLAO") {
                                    $("#cboDepMod option[value='14']").attr("selected", true);
                            }

                            if ($("#cboDepMod").val() == '') {
                                controls.cboDepMod.prop('disabled', false);
                            }

                                that.InitProvincias(itemSelect);
                            }
                        },
                        error: function (data) {
                            alert('No se ha encontrado el departamento de la dirección señalada.');
                        }
                    });
                }
            });
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

        ContadorD1: function () {
            var objTipoVia = document.getElementById('cboTipoVia');
            var objNombreCalle = document.getElementById('txtNomVia');
            var objNroCalle = document.getElementById('txtNumeroCalle');
            var objCheck = document.getElementById('chkSN');
            var objTipoManzana = document.getElementById('cboTipoMz');
            var objManzana = document.getElementById('txtNroMz');
            var objLote = document.getElementById('txtLote');
            var objTipoInterior = document.getElementById('cboTipoInterior');
            var objNroInterior = document.getElementById('txtInterior');

            var objTipoUrbanizacion = document.getElementById('cboTipoUrb');
            var objNombreUrbanizacion = document.getElementById('txtUrb');
            var objTipoZona = document.getElementById('cboTipoZona');
            var objNombreEtapa = document.getElementById('txtZona');
            var objReferencias = document.getElementById('txtReferencia');

            strCad = '';

            if (objTipoVia.selectedIndex > 0 && objTipoVia.value != '') {
                strCad = strCad + objTipoVia.options[objTipoVia.selectedIndex].value;
            }
            if (objNombreCalle.value != '') {
                strCad = strCad + ' ' + objNombreCalle.value;
            }

            if (objNroCalle.value != '') {
                if (objNroCalle.value != 'S/N')
                { strCad = strCad + ' ' + objNroCalle.value; } else { strCad = strCad + ' ' + 'S/N'; }
            } else if (objNroCalle.disabled != false) {
                strCad = strCad
            }
            if (objCheck.checked) {
                strCad = strCad + ' ' + 'S/N';
                //objNroCalle.disabled = true;
            }

            if (objTipoManzana.selectedIndex < 1) {
                objManzana.value = '';
                strCad = strCad;
                //objManzana.disabled = true;
            } else {
                //objManzana.disabled = false;
                strCad = strCad + ' ' + objTipoManzana.value + ' ' + objManzana.value;
            }

            if (objTipoInterior.selectedIndex < 1) {
                objNroInterior.value = '';
                //objNroInterior.disabled = true;
                strCad = strCad;
            } else {
                //objNroInterior.disabled = false;
                strCad = strCad + ' ' + objTipoInterior.value + ' ' + objNroInterior.value;
            }

            if (objTipoManzana.value == 'MZ') {
                //objNroCalle.value = '';
                //objNroCalle.disabled = true;
                //objLote.disabled = false;
                strCad = strCad + ' ' + objTipoManzana.value + ' ' + objManzana.value + ' ' + objLote.value;
            }
            else {
                //objNroCalle.disabled = false;
                objLote.value = '';
                //objLote.disabled = true;
                strCad = strCad;
            }


           

            if (objTipoUrbanizacion.selectedIndex < 1) {
               // objNombreUrbanizacion.value = '';
                //objNombreUrbanizacion.disabled = true;
            }
            if (objTipoZona.selectedIndex < 1) {
                objNombreEtapa.value = '';
                //objNombreEtapa.disabled = true;
            } else {
                //objNombreEtapa.disabled = false;
            }
            if (objTipoUrbanizacion.selectedIndex > 0 && objTipoUrbanizacion.value != '') {
                strCad = strCad + ' ' + objTipoUrbanizacion.options[objTipoUrbanizacion.selectedIndex].value;
            }
            if (objNombreUrbanizacion.value != '') {
                strCad = strCad + ' ' + objNombreUrbanizacion.value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex > 0 && objTipoZona.value != '') {
                strCad = strCad + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex == 0) {
                strCad = strCad + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objNombreEtapa.value != '') {
                strCad = strCad + ' ' + objNombreEtapa.value;
            }

            strDireccion = strCad;
        },

        getRulesControls: function () {

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
                $('#frmChangeServiceAddress').validate({
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
            $('#frmChangeServiceAddress').validate({
                rules: {
                    nmcboTipoVia: {
                        required: true
                    },
                    nmtxtTipoVia: {
                        required: true
                    },
                    //nmcboTipoUrb: {
                    //    required: true
                    //},
                    //nmtxtUrb: {
                    //    required: true
                    //},
                    //nmcboTipoZona:{
                    //    required: true
                    //},
                    //nmtxtZona: {
                    //    required: true
                    //},
                    //nmtxtReferencia: {
                    //    required: true
                    //},
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
                    }

                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {
                    //nmcboTipoUrb:{
                    //    required: "** La opción es Requerido",
                    //},
                    //nmtxtUrb: {
                    //    required: "** La opción es Requerido",
                    //},
                    //nmcboTipoZona: {
                    //    required: "** La opción es Requerido",
                    //},
                    //nmtxtZona: {
                    //    required: "** La opción es Requerido",
                    //},
                    nmcboTipoVia: {
                        required: "* La opción es Requerido",
                    },
                    nmtxtTipoVia: {
                        required: "* El campo es Requerido",
                    },
                    //nmtxtReferencia: {
                    //    required: "** La opción es Requerido",
                    //},
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
            $('#frmChangeServiceAddress').validate({
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
            $('#frmChangeServiceAddress').validate({
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

        pad: function (s) { return (s < 10) ? '0' + s : s; },

        getUrl: window.location.protocol + '//' + window.location.host,
        
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

    $.fn.INTChangeServiceAddress = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTChangeServiceAddress'),
                options = $.extend({}, $.fn.INTChangeServiceAddress.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTChangeServiceAddress', data);
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

    $.fn.INTChangeServiceAddress.defaults = {
    }

    $('#divBody').INTChangeServiceAddress();
})(jQuery);