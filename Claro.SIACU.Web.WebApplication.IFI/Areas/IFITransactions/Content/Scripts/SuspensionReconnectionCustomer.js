(function ($, undefined) {
    //Variables para la session
    var DATACUSTOMER = {};
    var DATASERVICE = {};
    var USERACCESS = {};
    var URLPARAMS = {};
    var IDSESSION = 0;
    //Numero de Linea
    var MSISDN = "";
    var habilitaRetenidoSusp = "";
    //HPXTREAM
    var PATH_FILE_HPXTREAM = "";
    //Variables para inicio de carga de la pagina
    var OBJLOAD = {
        FlagContingenciaHP: "",
        flagRestringirAccesoTemporalSRASC: "",
        ConstMsgOpcionTemporalmenteInhabilitada: "",
        ConsLineaDesaActiva: "",
        ConstMsgLineaStatSuspe: "",
        DiasMinSuspension: "",
        MinDiasRetSuspension: "",
        MontoRet: "",
        ImpPagar: "",
        LoadMsgHiddens:
        {
            hdnTituloPagina: "",
            hdnMensaje1: "",
            hdnMensaje2: "",
            hdnMensaje3: "",
            hdnMensaje4: ""
        },
        EstadoContrato: {
            EstadoContratoInactivo: "",
            EstadoContratoSuspendido: "",
            EstadoContratoReservado: "",
            MsgValidacionContratoInactivo: "",
            MsgValidacionContratoReservado: ""
        },

        DiasMaxConRetencion: "",
        DiasMaxSinRetencion: "",
        DiasMinSinRetencion: ""
    };
    //Variables para tipificacion
    var TYPIFICATION = {
        ClaseId: "",
        SubClaseId: "",
        Tipo: "",
        ClaseDes: "",
        SubClaseDes: "",
        TipoId: "",
    };
    var Form = function ($element, options) {
        $.extend(this, $.fn.INTSuspensionReconnectionCustomer.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblContact: $('#lblContact', $element)
            , lblLegalRepresentative: $('#lblLegalRepresentative', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , lblActivationDate: $('#lblActivationDate', $element)
            , txtDateSuspension: $('#txtDateSuspension', $element)
            , txtDateReact: $('#txtDateReact', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , txtNotas: $('#txtNotas', $element)
            , btnClose: $('#btnClose', $element)
            , calendartxtStartDate: $('#calendartxtStartDate', $element)
            , calendartxtEndDate: $('#calendartxtEndDate', $element)
            , lblTitle: $('#lblTitle', $element)
            , lblAddress: $('#lblAddress', $element)
            , lblNoteAddress: $('#lblNoteAddress', $element)
            , lblDepartment: $('#lblDepartment', $element)
            , lblCountry: $('#lblCountry', $element)
            , lblDistrict: $('#lblDistrict', $element)
            , lblProvince: $('#lblProvince', $element)
            , lblPlaneCode: $('#lblPlaneCode', $element)
            , lblInvoiceUbigeo: $('#lblInvoiceUbigeo', $element)
            , lblContrato: $('#lblContrato', $element)
            , lblCustomerId: $('#lblCustomerId', $element)
            , lblCustomerType: $('#lblCustomerType', $element)
            , lblPlan: $('#lblPlan', $element)
            , lblBillingCycle: $('#lblBillingCycle', $element)
            , lblLimitCredit: $('#lblLimitCredit', $element)
            , txtImpPagar: $('#txtImpPagar', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnSave: $('#btnSave', $element)
            , divRules: $('#divRules', $element)
            , chkRetencion: $('#chkRetencion', $element)
            , chkEmail: $('#chkEmail', $element)
            , txtEmail: $('#txtEmail', $element)
            , lblMensaje: $('#lblErrorMessage', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , closeErrorAlert: $('#closeErrorAlert', $element)
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            var d = new Date();

            var dayOfMonth = d.getDate();
            d.setDate(dayOfMonth + 1);
            controls.txtDateSuspension.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateSuspension.datepicker("setDate", d);
            controls.txtDateReact.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.chkRetencion.addEvent(that, 'click', that.paymentOCC);
            controls.chkEmail.addEvent(that, 'click', that.chkEmail_click);
            controls.closeErrorAlert.addEvent(that, 'click', that.closeErrorAlert_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.txtDateReact.on("change", function () { that.validateDates(); });
            controls.txtDateSuspension.on("change", function () { that.validateDatesSuspen(); });

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();

        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this;
            var controls = that.getControls();
            that.Loading();
            that.getLoadSession();
            that.getCACDAC();
            controls.chkEmail.attr("checked", true);
            if (controls.chkEmail.is(':checked')) {
                controls.txtEmail.prop('disabled', false);
                controls.txtEmail.css('display', 'block');
            } else {
                controls.txtEmail.prop('disabled', true);
                controls.txtEmail.css('display', 'none');
            }

        },
        chkEmail_click: function () {
            var that = this;
            var controls = that.getControls();

            if (controls.chkEmail.is(':checked')) {
                controls.txtEmail.prop('disabled', false);
                controls.txtEmail.css('display', 'block');
            } else {
                controls.txtEmail.prop('disabled', true);
                controls.txtEmail.css('display', 'none');
            }
        },
        btnClose_click: function () {
            parent.window.close();
        },
        closeErrorAlert_click: function () {
            var that = this;
            var controls = that.getControls();
            controls.divErrorAlert.hide();
        },
        btnConstancy_click: function (sender, arg) {

            if (PATH_FILE_HPXTREAM == "" || PATH_FILE_HPXTREAM == undefined || PATH_FILE_HPXTREAM == null) {
                alert("HPXTREAM NO DISPONIBLE!!!", "Alerta");
            } else {
                try {
                    ReadRecordSharedFile(IDSESSION, PATH_FILE_HPXTREAM);
                } catch (e) {
                    alert('Ocurrió un error en la previsualización de la constancia.', "Alerta");
                }
            }
        },
        getImpPagarSinIgv: function () {
            var that = this;
            var controls = that.getControls();
            var imp = controls.txtImpPagar.val();
            var igv = (imp * 18) / 100;
            return imp - igv;
        },
        btnSave_click: function () {
            try {
                var that = this;
                var controls = that.getControls();

                var objParams = {
                    dateSuspension: controls.txtDateSuspension.val(),
                    dateReactivation: controls.txtDateReact.val(),
                    msisdn: MSISDN,
                    contractId: DATACUSTOMER.ContractID,
                    currentUser: USERACCESS.login,
                    fullNameUser: USERACCESS.fullName,
                    chkRetencion: controls.chkRetencion.is(':checked'),
                    customerId: DATACUSTOMER.CustomerID,
                    tipo: TYPIFICATION.Tipo,
                    claseDes: TYPIFICATION.ClaseDes,
                    subClaseDes: TYPIFICATION.SubClaseDes,
                    claseCode: TYPIFICATION.ClaseId,
                    subClaseCode: TYPIFICATION.SubClaseId,
                    tipoCode: TYPIFICATION.TipoId,
                    notes: controls.txtNotas.val(),

                    impPagar: controls.txtImpPagar.val(),

                    imPagarsinIGV: that.getImpPagarSinIgv(),
                    montoRet: controls.chkRetencion.is(':checked') ? OBJLOAD.ImpPagar : OBJLOAD.MontoRet,

                    idSession: IDSESSION,
                    account: DATACUSTOMER.Account,
                    name: DATACUSTOMER.Name,
                    lastName: DATACUSTOMER.LastName,
                    businessName: DATACUSTOMER.BusinessName,
                    documentType: DATACUSTOMER.DocumentType,
                    documentNumber: DATACUSTOMER.DocumentNumber,
                    address: DATACUSTOMER.Address,
                    district: DATACUSTOMER.District,
                    departament: DATACUSTOMER.Departament,
                    province: DATACUSTOMER.Province,
                    codPlano: DATACUSTOMER.PlaneCodeInstallation,
                    fullName: DATACUSTOMER.FullName,
                    legalAgent: DATACUSTOMER.LegalAgent,
                    tipoCliente: DATACUSTOMER.CustomerType,
                    cboCACDAC: $('#ddlCACDAC option:selected').text(),
                    email: controls.txtEmail.val(),
                    chkEmail: controls.chkEmail.is(':checked'),

                    diasMaxConRetencion: OBJLOAD.DiasMaxConRetencion,
                    diasMaxSinRetencion: OBJLOAD.DiasMaxSinRetencion,
                    diasMinSinRetencion: OBJLOAD.DiasMinSinRetencion
                };


                //Para validar fechas
                if (!that.validateDates()) {
                    return false;
                }

                //Para validar cantidad en las notas
                if (!that.validateInteraction()) {
                    return false;
                }

                if (controls.ddlCACDAC.val() == "-1") {
                    alert("Seleccione CAC/DAC", "Alerta");
                    return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidate = regx.test(controls.txtEmail.val());
                if (controls.chkEmail.is(':checked')) {
                    if (blvalidate == false) {
                        alert('Ingresar un Email Válido.', "Alerta");
                        return false;
                    }
                }


                confirm(OBJLOAD.LoadMsgHiddens.hdnMensaje1, "Confirmación",
                    function (result) {
                        if (!result) {
                            return false;
                        } else {

                            that.Loading();
                            $.app.ajax({
                                type: 'POST',
                                contentType: "application/json; charset=utf-8",
                                dataType: 'json',
                                data: JSON.stringify(objParams),
                                url: '/IFITransactions/SuspensionReconnectionCustomer/SaveTransaccion',
                                error: function (ex) {
                                    alert("Ha ocurrido un problema en la transacción, por favor volver a intentarlo más tarde.", "Alerta");
                                },
                                success: function (response) {

                                    //SET HPXTREAM
                                    PATH_FILE_HPXTREAM = response.PathFileHPxtream

                                    if (response.Result) {
                                        controls.btnSave.prop('disabled', true);
                                        controls.btnConstancy.prop('disabled', false);
                                        controls.chkRetencion.prop('disabled', true);
                                        if (response.LblMsgView) {
                                            if (response.MsgError != "") {
                                                alert(response.MsgError);
                                                controls.lblMensaje.html(response.MsgError);
                                                controls.divErrorAlert.show();
                                            }
                                        } else {
                                            if (response.MsgError != "") {
                                                alert(response.MsgError);
                                            }
                                        }

                                    } else {
                                        controls.btnSave.prop('disabled', false);
                                        controls.btnConstancy.prop('disabled', true);
                                        controls.chkRetencion.prop('disabled', true);
                                        if (response.LblMsgView) {
                                            if (response.MsgError != "") {
                                                alert(response.MsgError);
                                                controls.lblMensaje.html(response.MsgError);
                                                controls.divErrorAlert.show();
                                            }
                                        } else {
                                            if (response.MsgError != "") {
                                                alert(response.MsgError);
                                            }
                                        }
                                    }
                                }
                            });
                        }
                    });
            } catch (e) {
                alert('Ocurrió un error al guardar en la transacción.', "Alerta");
            }
        },
        getLoadSession: function () {

            try {
                var that = this;
                var controls = that.getControls();

                var objparameters = JSON.parse(sessionStorage.getItem("SessionTransac"));


                DATACUSTOMER = objparameters.SessionParams.DATACUSTOMER;
                DATASERVICE = objparameters.SessionParams.DATASERVICE;
                USERACCESS = objparameters.SessionParams.USERACCESS;
                URLPARAMS = objparameters.UrlParams;

                IDSESSION = URLPARAMS.IDSESSION;

                that.getLoadDataCustomer();
                that.getPageLoad();

            } catch (e) {
                alert('Ocurrió un error al cargar la transacción.', "Alerta");
                $("#divBody input").prop("disabled", true);
                $("#divBody select").prop("disabled", true);
                $("#divBody textarea").prop("disabled", true);
                $("#divBody button").prop("disabled", true);
            }



        },
        getPageLoad: function () {
            try {
                var that = this;
                var controls = that.getControls();

                //Inizializar Importes a numericos
                controls.txtImpPagar.numeric();

                //INICIO - VALIDACIONES ESTADO
                if (OBJLOAD.flagRestringirAccesoTemporalSRASC == "1") {

                    alert(OBJLOAD.ConstMsgOpcionTemporalmenteInhabilitada, "Alerta", function () {
                        window.close();
                    });
                }

                if (USERACCESS == null || USERACCESS == undefined) {

                    window.close();
                }

                //PageLoad
                var parameters = {
                    idSession: IDSESSION,
                    currentUser: USERACCESS.login,
                    contractId: DATACUSTOMER.ContractID,
                    CadenaOpciones: USERACCESS.optionPermissions,
                    strCustomerId: DATACUSTOMER.CustomerID                  

                };


                $.app.ajax({
                    type: 'POST',
                    cache: false,
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(parameters),
                    url: '/IFITransactions/SuspensionReconnectionCustomer/PageLoad',
                    success: function (response) {


                        if (response.typificationLoad.result) {
                            if (response.typificationLoad.objResult != null) {
                                that.getLoadTypification(response.typificationLoad.objResult);
                            }

                        } else {
                            controls.divErrorAlert.show();
                            controls.lblMensaje.html(response.typificationLoad.lblMensaje);
                            controls.btnSave.prop('disabled', true);
                        }

                        that.getLoadMSG(response.msgHiddensLoad);
                        if (response.checkStatusScheduledLoad.Result == false) {
                            alert(response.checkStatusScheduledLoad.Msg, "Alerta", function () {
                                window.close();
                            });
                        }
                        else if (DATASERVICE.StateLine == OBJLOAD.ConsLineaDesaActiva) {

                            alert(OBJLOAD.ConstMsgLineaStatSuspe, "Alerta", function () {
                                window.close();
                            });
                        }
                        else if (DATASERVICE.StateLine == OBJLOAD.EstadoContrato.EstadoContratoInactivo) {

                            alert(OBJLOAD.EstadoContrato.MsgValidacionContratoInactivo, "Alerta", function () {
                                window.close();
                            });
                        }
                        else if (DATASERVICE.StateLine == OBJLOAD.EstadoContrato.EstadoContratoReservado) {

                            alert(OBJLOAD.EstadoContrato.MsgValidacionContratoReservado, "Alerta", function () {
                                window.close();
                            });
                        }
                        else if (DATASERVICE.Reason == "Moroso") {

                            alert("La línea se encuentra suspendida por morosidad, no es posible realizar la transacción", "Alerta", function () {
                                window.close();
                            });
                        }
                        else if (DATASERVICE.StateLine == "Cancelado") {

                            alert("La línea de encuentra cancelada, no es posible realizar la transacción", "Alerta", function () {
                                window.close();
                            });
                        }
                        else if (!response.resulValidateLineBlock) {
                            alert("La línea de encuentra bloqueada, no es posible realizar la transacción", "Alerta", function () {
                                window.close();
                            });
                        }


                        MSISDN = response.msisdn;
                        habilitaRetenidoSusp = response.habilitaRetenido;
                        that.getBusinessRules();
                    }
                });
            } catch (e) {
                alert('Ocurrió un error al cargar la transacción.', "Alerta");
                $("#divBody input").prop("disabled", true);
                $("#divBody select").prop("disabled", true);
                $("#divBody textarea").prop("disabled", true);
                $("#divBody button").prop("disabled", true);
            }
        },
        getLoadTypification: function (objTypification) {

            TYPIFICATION.ClaseId = objTypification.CLASE_CODE;
            TYPIFICATION.SubClaseId = objTypification.SUBCLASE_CODE;
            TYPIFICATION.Tipo = objTypification.TIPO;
            TYPIFICATION.ClaseDes = objTypification.CLASE;
            TYPIFICATION.SubClaseDes = objTypification.SUBCLASE;
            TYPIFICATION.TipoId = objTypification.TIPO_CODE;
        },
        getLoadMSG: function (objMsg) {
            var that = this;
            var controls = that.getControls();

            OBJLOAD.FlagContingenciaHP = objMsg.FlagContingenciaHP;
            OBJLOAD.flagRestringirAccesoTemporalSRASC = objMsg.flagRestringirAccesoTemporalSRASC;
            OBJLOAD.ConstMsgOpcionTemporalmenteInhabilitada = objMsg.ConstMsgOpcionTemporalmenteInhabilitada;
            OBJLOAD.ConsLineaDesaActiva = objMsg.ConsLineaDesaActiva;
            OBJLOAD.ConstMsgLineaStatSuspe = objMsg.ConstMsgLineaStatSuspe;
            OBJLOAD.DiasMinSuspension = objMsg.DiasMinSuspension;
            OBJLOAD.MinDiasRetSuspension = objMsg.MinDiasRetSuspension;

            OBJLOAD.MontoRet = objMsg.MontoRet;
            OBJLOAD.ImpPagar = objMsg.ImpPagar;


            OBJLOAD.LoadMsgHiddens.hdnTituloPagina = objMsg.LoadMsgHiddens.hdnTituloPagina;
            OBJLOAD.LoadMsgHiddens.hdnMensaje1 = objMsg.LoadMsgHiddens.hdnMensaje1;
            OBJLOAD.LoadMsgHiddens.hdnMensaje2 = objMsg.LoadMsgHiddens.hdnMensaje2;
            OBJLOAD.LoadMsgHiddens.hdnMensaje3 = objMsg.LoadMsgHiddens.hdnMensaje3;
            OBJLOAD.LoadMsgHiddens.hdnMensaje4 = objMsg.LoadMsgHiddens.hdnMensaje4;

            OBJLOAD.EstadoContrato.EstadoContratoInactivo = objMsg.EstadoContrato.EstadoContratoInactivo;
            OBJLOAD.EstadoContrato.EstadoContratoSuspendido = objMsg.EstadoContrato.EstadoContratoSuspendido;
            OBJLOAD.EstadoContrato.EstadoContratoReservado = objMsg.EstadoContrato.EstadoContratoReservado;
            OBJLOAD.EstadoContrato.MsgValidacionContratoInactivo = objMsg.EstadoContrato.MsgValidacionContratoInactivo;
            OBJLOAD.EstadoContrato.MsgValidacionContratoReservado = objMsg.EstadoContrato.MsgValidacionContratoReservado;

            OBJLOAD.DiasMaxConRetencion = objMsg.DiasMaxConRetencion;
            OBJLOAD.DiasMaxSinRetencion = objMsg.DiasMaxSinRetencion;
            OBJLOAD.DiasMinSinRetencion = objMsg.DiasMinSinRetencion;

            //Cargar Titulo
            $('#lblTitle').html(OBJLOAD.LoadMsgHiddens.hdnTituloPagina);

            //IMPORTE A PAGAR
            controls.txtImpPagar.val(OBJLOAD.ImpPagar);

            //EMAIL
            controls.txtEmail.val(DATACUSTOMER.Email || '');

        },
        getCACDAC: function () {
            var that = this,
            objCacDacType = {
                strIdSession: IDSESSION
            };

            var parameters = {};
            parameters.strIdSession = IDSESSION;
            parameters.strCodeUser = USERACCESS.login;

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
                            $("#ddlCACDAC").append($('<option>', { value: '-1', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    $("#ddlCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    $("#ddlCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
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
        getLoadDataCustomer: function () {
            var that = this;
            var controls = that.getControls();

            //Datos de Cliente
            controls.lblCustomerName.html(DATACUSTOMER.BusinessName);
            controls.lblContact.html(DATACUSTOMER.FullName);
            controls.lblLegalRepresentative.html(DATACUSTOMER.LegalAgent);
            controls.lblIdentificationDocument.html(DATACUSTOMER.DNIRUC);
            controls.lblActivationDate.html(DATASERVICE.ActivationDate);
            controls.lblContrato.html(DATACUSTOMER.ContractID);
            controls.lblCustomerId.html(DATACUSTOMER.CustomerID);
            controls.lblCustomerType.html(DATACUSTOMER.CustomerType);
            controls.lblPlan.html(DATASERVICE.Plan);
            controls.lblBillingCycle.html(DATACUSTOMER.BillingCycle);

            controls.lblLimitCredit.html(DATACUSTOMER.objPostDataAccount.CreditLimit);

            //Datos de Direccion
            controls.lblAddress.html(DATACUSTOMER.LegalAddress);
            controls.lblNoteAddress.html(DATACUSTOMER.Reference);
            controls.lblDepartment.html(DATACUSTOMER.DEPARTEMENTO_LEGAL);
            controls.lblCountry.html(DATACUSTOMER.PAIS_LEGAL);

            controls.lblDistrict.html(DATACUSTOMER.District);
            controls.lblProvince.html(DATACUSTOMER.Province);
            controls.lblPlaneCode.html(DATACUSTOMER.CODIGO_PLANO_FACT);
            controls.lblInvoiceUbigeo.html(DATACUSTOMER.UBIGEO_FACT);

            //Desabilitar Controller
            controls.btnConstancy.prop('disabled', true);
            controls.txtEmail.prop('disabled', true);
        },
        getBusinessRules: function () {
            var that = this;
            var controls = that.getControls();
            var objRules = {
                strIdSession: IDSESSION,
                strSubClase: TYPIFICATION.SubClaseId
            };

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/IFITransactions/CommonServices/GetBusinessRules',
                data: JSON.stringify(objRules),
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    }
                }
            });
        },
        getRoute: function () {
            return window.location.protocol + '//' + window.location.host;
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
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        //Validate
        validateDatesSuspen: function () {
            var that = this;
            var controls = that.getControls();
            var fechaSus = controls.txtDateSuspension.val();
            fechaSus = fechaSus.substr(6, 4) + "/" + fechaSus.substr(3, 2) + "/" + fechaSus.substr(0, 2);
            var fechaRea = controls.txtDateReact.val();
            fechaRea = fechaRea.substr(6, 4) + "/" + fechaRea.substr(3, 2) + "/" + fechaRea.substr(0, 2);
            if (fechaSus == "//") {
                alert("Necesita seleccionar las fechas de suspensión y reactivación");
                return false;
            }
            var fsus = new Date(fechaSus);
            var frea = new Date(fechaRea);
            var today = new Date();

            if (frea <= fsus) {
                alert("La fecha de suspensión debe ser menor a la fecha de reactivación");
                controls.txtDateSuspension.val('');
                return false;
            }
            if (fsus <= today) {
                alert("La fecha de suspensión debe ser mayor al día de hoy");
                controls.txtDateSuspension.val('');
                return false;
            }
            return true;
        },
        validateDates: function () {
            var that = this;
            var controls = that.getControls();

            var fechaSus = controls.txtDateSuspension.val();
            fechaSus = fechaSus.substr(6, 4) + "/" + fechaSus.substr(3, 2) + "/" + fechaSus.substr(0, 2);

            var fechaRea = controls.txtDateReact.val();
            fechaRea = fechaRea.substr(6, 4) + "/" + fechaRea.substr(3, 2) + "/" + fechaRea.substr(0, 2);

            if (fechaSus == "//" || fechaRea == "//") {
                alert("Necesita seleccionar las fechas de suspensión y reactivación");
                return false;
            }

            var fsus = new Date(fechaSus);
            var frea = new Date(fechaRea);
            var today = new Date();

            if (frea <= today) {
                alert("La fecha de reactivacion debe ser mayor al día de hoy");
                controls.txtDateReact.val('');
                return false;
            }

            if (frea <= fsus) {
                alert("La fecha de reactivación debe ser mayor a la fecha de suspensión");
                controls.txtDateReact.val('');
                return false;
            }

            var oneDay = 24 * 60 * 60 * 1000;
            var diffDays = Math.round(Math.abs((fsus.getTime() - frea.getTime()) / (oneDay)));
            var dia1 = OBJLOAD.DiasMinSuspension;
            var dia2 = OBJLOAD.DiasMaxConRetencion;
            var dia3 = OBJLOAD.DiasMaxConRetencion;
            var dia4 = OBJLOAD.MinDiasRetSuspension;

            if (!controls.chkRetencion.is(":checked")) {
                if (diffDays < dia1) {
                    alert("La fecha de suspensión debe ser por lo menos menor por " + dia1 + " días de la fecha de reactivación");
                    controls.txtDateReact.val('');
                    return false;
                }

            } else {
                if (diffDays < dia4) {
                    alert("La fecha de retencion debe ser por lo menos menor por " + dia4 + " días de la fecha de reactivación");
                    controls.txtDateReact.val('');
                    return false;
                }

                if (diffDays > dia3) {
                    alert("El periodo máximo de suspensión por retención no puede superar a los " + dia3 + " días. No es posible realizar la transacción.");
                    controls.txtDateReact.val('');
                    return false;
                }
            }
            var objModel = {
                dateSuspension: controls.txtDateSuspension.val(),
                dateReactivation: controls.txtDateReact.val(),
                chkRetencion: controls.chkRetencion.is(':checked'),
                diasMaxConRetencion: OBJLOAD.DiasMaxConRetencion,
                diasMaxSinRetencion: OBJLOAD.DiasMaxSinRetencion,
                diasMinSinRetencion: OBJLOAD.DiasMinSinRetencion
            };
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: '/IFITransactions/SuspensionReconnectionCustomer/ValidateRangesDates',
                error: function (ex) {
                    alert("Ha ocurrido un problema en la validacion de las fechas , por favor volver a intentarlo más tarde.", "Alerta");
                },
                success: function (response) {


                    if (response.state == false && response.outMsg != "") {
                        alert(response.outMsg, "Alerta");
                        controls.txtDateReact.val('');
                        return false;
                    }
                },
                complete: function () {

                }

            });

            return true;
        },
        validateInteraction: function () {
            var that = this;
            var controls = that.getControls();

            var strNotas = controls.txtNotas.val();

            if (strNotas.length > 3800) {
                alert(OBJLOAD.LoadMsgHiddens.hdnMensaje4);
                controls.txtNotas.val(strNotas.substring(0, 3800));
                controls.txtNotas.focus();
                return false;
            }
            return true;
        },
        paymentOCC: function () {
            var that = this;
            var controls = that.getControls();

            if (controls.chkRetencion.is(':checked')) {
                $("#txtImpPagar").removeAttr("disabled");
                controls.txtImpPagar.val(OBJLOAD.MontoRet);

                if (!habilitaRetenidoSusp) {
                    ValidateUserIFI('strKeyPerfSuspHabilitaRetenido', function () { habilitaRetenidoSusp = true }, null, null, null);
                    if (!habilitaRetenidoSusp) {

                        controls.chkRetencion.prop('checked', false);
                        controls.txtImpPagar.val(OBJLOAD.ImpPagar);
                    }
                }
            } else {
                $("#txtImpPagar").attr("disabled", "disabled");
                var _dateReact = $("#txtDateReact").val();
                if (_dateReact.trim() != '' && _dateReact != null) {
                    that.validateDates();
                }
                controls.txtImpPagar.val(OBJLOAD.ImpPagar);
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
        }
    },


    $.fn.INTSuspensionReconnectionCustomer = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('INTSuspensionReconnectionCustomer'),
                options = $.extend({}, $.fn.INTSuspensionReconnectionCustomer.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTSuspensionReconnectionCustomer', data);
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
    $.fn.INTSuspensionReconnectionCustomer.defaults = {
    }
    $('#divBody').INTSuspensionReconnectionCustomer();
})(jQuery);