(function ($, undefined) {
    //Variables para la session
    var DATACUSTOMER = {};
    var DATASERVICE = {};
    var USERACCESS = {};
    var URLPARAMS = {};
    var IDSESSION = 0;
    //Numero de Linea
    var MSISDN = "";
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
        $.extend(this, $.fn.ContentEditSuspension.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element

            , txtImpPagar: $('#txtImpPagar', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnSave: $('#btnSave', $element)
            , txtDateSuspension: $('#txtDateSuspension', $element)
            , txtDateReact: $('#txtDateReact', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , txtNotas: $('#txtNotas', $element)
            , btnClose: $('#btnClose', $element)
            , calendartxtStartDate: $('#calendartxtStartDate', $element)
            , calendartxtEndDate: $('#calendartxtEndDate', $element)
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
            controls.txtDateSuspension.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateReact.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.chkRetencion.addEvent(that, 'click', that.paymentOCC);
            controls.chkEmail.addEvent(that, 'click', that.chkEmail_click);
            controls.closeErrorAlert.addEvent(that, 'click', that.closeErrorAlert_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
           

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

        btnSave_click: function () {
            try {
                var that = this;
                var controls = that.getControls();
                var objsModifySusp = JSON.parse(sessionStorage.getItem("Wsuspension"));


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
                    montoRet: controls.chkRetencion.is(':checked') ? OBJLOAD.ImpPagar : OBJLOAD.MontoRet,

                    idSession: Session.IDSESSION,
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
                    strIdSession: Session.IDSESSION,
                    diasMaxConRetencion: OBJLOAD.DiasMaxConRetencion,
                    diasMaxSinRetencion: OBJLOAD.DiasMaxSinRetencion,
                    diasMinSinRetencion: ((controls.txtDateSuspension.is(':disabled')) ? 0 : OBJLOAD.DiasMinSinRetencion),
                    objScheduledTransactionModel: objsModifySusp.objScheduledTransactionModel
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
                                url: '/IFITransactions/ScheduledTasks/EditTransaccion',
                                error: function (ex) {
                                    alert("Ha ocurrido un problema en la transacción, por favor volver a intentarlo más tarde.", "Alerta");
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
                                complete: function () {
                                    $.unblockUI();
                                },
                                success: function (response) {

                                    //SET HPXTREAM
                                    PATH_FILE_HPXTREAM = response.PathFileHPxtream

                                    if (response.Result) {
                                        controls.btnSave.prop('disabled', true);
                                        controls.btnConstancy.prop('disabled', false);


                                        if (response.MsgError != "") {
                                            alert(response.MsgError);
                                            controls.lblMensaje.html(response.MsgError);
                                            controls.divErrorAlert.show();
                                        }



                                    } else {
                                        controls.btnSave.prop('disabled', false);
                                        controls.btnConstancy.prop('disabled', true);


                                        if (response.MsgError != "") {
                                            alert(response.MsgError);
                                            controls.lblMensaje.html(response.MsgError);
                                            controls.divErrorAlert.show();
                                        }


                                    }
                                }
                            });
                        }
                    });
            } catch (e) {
                alert('Ocurrió un error al guardar en la transacción.', "Alerta");
                $.unblockUI();
            }
            $.unblockUI();
        },
        render: function () {
            var that = this;
            var controls = that.getControls();

            if (typeof sessionStorage.getItem("Wsuspension") != "undefined" && sessionStorage.getItem("Wsuspension") != null && sessionStorage.getItem("Wsuspension") != "") {
                var objModifySusp = JSON.parse(sessionStorage.getItem("Wsuspension"));
                if (typeof objModifySusp.objScheduledTransaction != "undefined" && objModifySusp.objScheduledTransaction != null) {
                    if (objModifySusp.objScheduledTransaction.Mode == "Mod") {

                        that.getDataSuspension(objModifySusp);
                    } else {
                        alert("Ud. no tiene permisos para Editar");
                    }
                } else {
                    alert("Ocurrio un problema al mostrar el formulario de Edicion");
                }



            } else {
                alert("Ocurrio un problema al mostrar el formulario de Edicion");
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

        getLoadSession: function () {


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
        getPageLoad: function () {
            try {
                var that = this;
                var controls = that.getControls();

                //Inizializar Importes a numericos
                controls.txtImpPagar.numeric();

                //INICIO - VALIDACIONES ESTADO
                if (DATASERVICE.StateLine == OBJLOAD.EstadoContrato.EstadoContratoInactivo) {

                    alert(OBJLOAD.EstadoContrato.MsgValidacionContratoInactivo, "Alerta", function () {
                        window.close();
                    });
                } else if (DATASERVICE.StateLine == OBJLOAD.EstadoContrato.EstadoContratoReservado) {

                    alert(OBJLOAD.EstadoContrato.MsgValidacionContratoReservado, "Alerta", function () {
                        window.close();
                    });
                }

                if (OBJLOAD.flagRestringirAccesoTemporalSRASC == "1") {

                    alert(OBJLOAD.ConstMsgOpcionTemporalmenteInhabilitada, "Alerta", function () {
                        window.close();
                    });
                }

                if (USERACCESS == null || USERACCESS == undefined) {

                    window.close();
                }

                if (DATASERVICE.StateLine == OBJLOAD.ConsLineaDesaActiva) {

                    alert(OBJLOAD.ConstMsgLineaStatSuspe, "Alerta", function () {
                        window.close();
                    });
                }

                var parameters = {
                    idSession: Session.IDSESSION,
                    currentUser: USERACCESS.login,
                    contractId: DATACUSTOMER.ContractID,
                    CadenaOpciones: USERACCESS.optionPermissions
                };


                $.app.ajax({
                    type: 'POST',
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

                        if (typeof sessionStorage.getItem("Wsuspension") == "undefined" || sessionStorage.getItem("Wsuspension") == null && sessionStorage.getItem("Wsuspension") == "") {

                            if (response.checkStatusScheduledLoad.Result == false) {
                                alert(response.checkStatusScheduledLoad.Msg, "Alerta", function () {
                                    window.close();
                                });
                            }
                        }
                        MSISDN = response.msisdn;

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
        getDataSuspension: function (parameters) {
            var that = this;
            var controls = that.getControls();
            var objDataSusp = {};
            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(parameters),
                url: '/IFITransactions/ScheduledTasks/GetEditScheduledTasks',
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
                    if (response.result == true) {
                        objDataSusp = response.data
                        if (objDataSusp != null) {
                            if (objDataSusp.dateReactivation == null)
                                objDataSusp.dateReactivation = "";
                            if (objDataSusp.dateSuspension == null)
                                objDataSusp.dateSuspension = "";


                            var parseddateReactivation = $.datepicker.parseDate('dd/mm/yy', objDataSusp.dateReactivation);
                            var parseddateSuspension = $.datepicker.parseDate('dd/mm/yy', objDataSusp.dateSuspension);
                            $("#txtDateReact").val(objDataSusp.dateReactivation);
                            $('#txtDateReact').datepicker('setDate', parseddateReactivation);
                            $("#txtDateSuspension").prop("disabled", !objDataSusp.dateSuspensionEnabled);

                            $('#txtDateSuspension').datepicker('setDate', parseddateSuspension);
                            $("#txtDateSuspension").val(objDataSusp.dateSuspension);

                            console.log($("#txtDateSuspension").datepicker("getDate"));

                            $("#chkRetencion").attr('checked', objDataSusp.chkRetencion);
                            $("#txtImpPagar").val(objDataSusp.impPagar);
                            $("#txtImpPagar").prop("disabled", !objDataSusp.impPagarEnabled);
                            $("#chkRetencion").attr("disabled", !objDataSusp.chkRetencionEnabled);
                            if (objDataSusp.objScheduledTransactionModel != null) {
                                var objsModifySusp = JSON.parse(sessionStorage.getItem("Wsuspension"));

                                objsModifySusp.objScheduledTransactionModel = objDataSusp.objScheduledTransactionModel;
                                sessionStorage.setItem("Wsuspension", JSON.stringify(objsModifySusp));
                            }

                        }

                        that.getLoadSession();
                        that.getCACDAC();
                    }

                },
                complete: function () {
                    that.putEmail(DATACUSTOMER.Email);
                    controls.txtDateReact.on("change", function () { that.validateDates(); });
                    controls.txtDateSuspension.on("change", function () { that.validateDatesSuspen(); });
                    $.unblockUI();


                }
            });
        },
        putEmail: function (email) {
            var that = this;
            var controls = that.getControls();
            controls.chkEmail.attr("checked", true);
            if (controls.chkEmail.is(':checked')) {
                controls.chkEmail.prop('disabled', false);
                controls.txtEmail.prop('disabled', false);
                controls.txtEmail.val(email);
            } else {
                controls.chkEmail.prop('disabled', false);
                controls.txtEmail.prop('disabled', false);
                controls.txtEmail.val("");
                controls.txtEmail.hide();
            }
        },
        paymentOCC: function () {
            var that = this;
            var controls = that.getControls();

            if (controls.chkRetencion.is(':checked')) {
                $("#txtImpPagar").removeAttr("disabled");
                controls.txtImpPagar.val(OBJLOAD.MontoRet);
            } else {
                $("#txtImpPagar").attr("disabled", "disabled");
                controls.txtImpPagar.val(OBJLOAD.ImpPagar);
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





        },
        getCACDAC: function () {
            var that = this,
            objCacDacType = {
                strIdSession: Session.IDSESSION
            };

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = USERACCESS.login;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',

                success: function (results) {
                    var cacdac = results.Cac;
                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
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


            //Desabilitar Controller
            controls.btnConstancy.prop('disabled', true);
            controls.txtEmail.prop('disabled', true);
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
            var today = new Date();
            var frea = new Date(fechaRea);
            var oneDay = 24 * 60 * 60 * 1000;
            var diffDays = Math.round(Math.abs((fsus.getTime() - frea.getTime()) / (oneDay)));

            if (today > fsus) {
                alert("Necesita ingresar una fecha a partir de hoy");
                controls.txtDateSuspension.val('');
                return false;
            }
            if (frea <= fsus) {
                alert("La fecha de suspensión debe ser menor a la fecha de reactivación");
                controls.txtDateSuspension.val('');
                return false;
            }

            return true;
        },

        validateDates: function () {
            var that = this;
            var controls = that.getControls();

            var fechaSus = controls.txtDateSuspension.val();

            console.log("salute");
            console.log($("#txtDateSuspension").datepicker("getDate"));
            console.log(controls.txtDateSuspension.val());
            fechaSus = fechaSus.substr(6, 4) + "/" + fechaSus.substr(3, 2) + "/" + fechaSus.substr(0, 2);
            console.log(23242342);
            console.log(fechaSus);
            var fechaRea = controls.txtDateReact.val();

            fechaRea = fechaRea.substr(6, 4) + "/" + fechaRea.substr(3, 2) + "/" + fechaRea.substr(0, 2);
            console.log(fechaRea);
            if (fechaSus == "//" || fechaRea == "//") {
                console.log("dd")
                alert("Necesita seleccionar las fechas de suspensión y reactivación");
                return false;
            }

            var fsus = new Date(fechaSus);
            var frea = new Date(fechaRea);
            var today = new Date();



            if (frea <= fsus) {
                alert("La fecha de suspensión debe ser menor a la fecha de reactivación");
                controls.txtDateReact.val('');
                return false;
            }

            var oneDay = 24 * 60 * 60 * 1000;
            var diffDays = Math.round(Math.abs((fsus.getTime() - frea.getTime()) / (oneDay)));
            var dia1 = 1;
            var dia2 = OBJLOAD.DiasMaxSinRetencion;
            var dia3 = OBJLOAD.DiasMaxConRetencion;
            var dia4 = OBJLOAD.MinDiasRetSuspension;

            if (!controls.chkRetencion.is(":checked")) {
                console.log(88888);
                console.log(diffDays);
                console.log(dia2);
                if (diffDays > dia2) {
                    console.log(999);
                    alert("El período mínimo y máximo de Suspensión es de " + dia1 + " y " + dia2 + " días respectivamente. No es posible realizar la transacción.");
                    controls.txtDateReact.val('');
                    return false;
                }
                console.log(7777);
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
                diasMinSinRetencion: ((controls.txtDateSuspension.is(':disabled')) ? 0 : OBJLOAD.DiasMinSinRetencion)
            };
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: '/IFITransactions/SuspensionReconnectionCustomer/ValidateRangesDatesEdit',
                error: function (ex) {
                    alert("Ha ocurrido un problema en la validacion de las fechas , por favor volver a intentarlo más tarde.", "Alerta");
                },
                success: function (response) {


                    if (response.state == false && response.outMsg != "") {
                        alert(response.outMsg, "Alerta");
                        controls.txtDateReact.val('');
                        return false;
                    }
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
            hsize = hsize;
            $('#content').css({ 'height': hsize + 'px' });
        }
    },


$.fn.ContentEditSuspension = function () {
    var option = arguments[0],
        args = arguments,
        value,
        allowedMethods = [];

    this.each(function () {

        var $this = $(this),
            data = $this.data('ContentEditSuspension'),
            options = $.extend({}, $.fn.ContentEditSuspension.defaults,
                $this.data(), typeof option === 'object' && option);

        if (!data) {
            data = new Form($this, options);
            $this.data('ContentEditSuspension', data);
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
    $.fn.ContentEditSuspension.defaults = {
    }
    $('#ContentEditSuspension').ContentEditSuspension();
})(jQuery);