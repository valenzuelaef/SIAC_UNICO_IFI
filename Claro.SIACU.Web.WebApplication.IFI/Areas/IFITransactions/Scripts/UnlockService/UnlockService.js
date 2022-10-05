var thot;
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.UnlockServiceForm.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
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
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , btnClose: $("#btnClose", $element)
            , btnSave: $('#btnSave', $element)
            , chkBloqueoEquipo: $("#chkBloqueoEquipo", $element)
            , chkBloqueoLinea: $("#chkBloqueoLinea", $element)
            , idBloqueoLineaPanel: $("#idBloqueoLineaPanel", $element)
            , idBloqueoEquipoPanel: $("#idBloqueoEquipoPanel", $element)
            , btnSearchIMEI: $("#btnSearchIMEI", $element)
            , exampleModal: $("#exampleModal", $element)
            , cbDocumentType: $("#cbDocumentType", $element)
            , pnlSolicitante: $("#pnlSolicitante", $element)
            , ddlMotive: $("#ddlMotive", $element)
            , NmrdbSolicitante: $("input:radio[name=NmrdbSolicitante]", $element)
            , txtFirstName: $("#txtFirstName", $element)
            , txtLastName: $("#txtLastName", $element)
            , txtDocumentNumber: $("#txtDocumentNumber", $element)
            , cbTitularRelation: $("#cbTitularRelation", $element)
            , txtOtherRelation: $("#txtOtherRelation", $element)
            , txtPhone: $("#txtPhone", $element)
            , txtParient: $("#txtParient", $element)
            , lblTitularRelation: $("#lblTitularRelation", $element)
            , lblOtherRelation: $("#lblOtherRelation", $element)
            , lblPhone: $("#lblPhone", $element)
            , lblParient: $("#lblParient", $element)
            , btnTest: $("#btnTest", $element)
            , pnlPreguntas: $("#pnlPreguntas", $element)
            , tblLineLock: $("#tblLineLock", $element)
            , tblEquipmentLock: $("#tblEquipmentLock", $element)
            , txtImei: $("#txtImei", $element)
            , chkEnvio: $("#chkEnvio", $element)
            , txtCorreo: $("#txtCorreo", $element)
            , lblEnvio: $("#lblEnvio", $element)
            , btnConstancy: $("#btnConstancy", $element)
            , hdFlagBiometric: $("#hdFlagBiometric", $element)
            , btnClean: $("#btnClean", $element)

        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            $("#lblTitle").text("Desbloqueo de Servicio");
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.chkBloqueoEquipo.addEvent(that, 'click', that.chkBloqueoEquipo_Click);
            controls.chkBloqueoLinea.addEvent(that, 'click', that.chkBloqueoLinea_Click);
            controls.btnSearchIMEI.addEvent(that, 'click', that.btnSearchIMEI_Click);
            controls.NmrdbSolicitante.addEvent(that, "click", that.NmrdbSolicitante_click);
            controls.btnTest.addEvent(that, "click", that.TestSecurity);
            controls.chkEnvio.addEvent(that, "click", that.ValidatechkEnvio);
            controls.btnConstancy.addEvent(that, "click", that.btnConstancy_click);
            controls.btnClose.addEvent(that, "click", that.btnClose_click);
            controls.btnClean.addEvent(that, "click", that.btnClean_click);

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();

        },
        btnClean_click: function () {
            var that = this;


            that.render();


        },
        btnClose_click: function () {
            parent.window.close();
        },
        btnConstancy_click: function () {
            var that = this;

            var PDFRoute = that.RutaArchivo;
            var IdSession = Session.IDSESSION;
            if (PDFRoute != "") {
                ReadRecordSharedFile(IdSession, PDFRoute);
            } else {
                alert("No se pudo mostrar la constancia", "Alerta");
            }
        },
        RutaArchivo: "",
        ValidatechkEnvio: function () {
            var that = this,
           controls = this.getControls();
            if (controls.chkEnvio.is(':checked')) {
                controls.txtCorreo.show();
                controls.lblEnvio.hide();
            } else {
                controls.lblEnvio.show();
                controls.txtCorreo.hide();
            }

        },
        ValidateStatusLine: function () {
            var that = this;
            var controls = that.getControls();
            var statusLine = Session.DATALINEA.StatusLinea;
            if (statusLine == "Desactivo") {
                controls.chkBloqueoLinea.attr('checked', false);
                that.chkBloqueoLinea_Click();
                alert("La línea no puede ser Desbloqueada debido a que se encuentra Inactiva", 'Alerta', function () { parent.window.close(); }); return;
            } if (!that.IsLockedLine()) {
                controls.chkBloqueoLinea.attr("checked", false);
                alert("La línea se encuentra Desbloqueada", 'Alerta'); return;
            }
        },
        IsLockedLine: function () {
            var that = this,
            controls = that.getControls();

            var result = false;

            $.each(that.lstLinesLocks, function (key, value) {

                if (value._tipo == "BLOQ_ROB" || value._tipo == "BLOQ_PER") result = true;
            });
            return result;
        },
        IniLoadPage: function () {

            var that = this,
                controls = that.getControls(),

            objType = {};


            objType.CadenaOpciones = Session.USERACCESS.optionPermissions;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: '/IFITransactions/UnlockService/GetPermisionUnLockService',
                async: true,
                error: function (data) {
                    alert("Error JS : en llamar al GetPermisionUnLockService.", "Alerta");
                },
                success: function (response) {
                    that.canLock = response.data;


                },
            });
        },

        getRelationParient: function () {

            var that = this,
           controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: that.strUrlLock + '/GetRelationParient',
                success: function (response) {
                    controls.cbTitularRelation.append($('<option>', { value: '', html: 'Seleccionar' }));

                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cbTitularRelation.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                        });
                    }
                }
            });

        },
        strUrlLock: '/IFITransactions/ServiceLock',
        setSolicitante: function () {
            var that = this,
                controls = that.getControls();
            controls.txtFirstName.val(Session.CLIENTE.Name);
            controls.txtLastName.val(Session.CLIENTE.LastName);
            controls.txtDocumentNumber.val(Session.DATACUSTOMER.Nro_Doc);
            $("#cbDocumentType option:contains(" + Session.DATACUSTOMER.TipoDocumento + ")").attr('selected', true);

        },
        cleanSolicitante: function () {
            var that = this,
                controls = that.getControls();
            controls.txtFirstName.val("");
            controls.txtLastName.val("");
            controls.txtDocumentNumber.val("");
            $("#cbDocumentType option:selected").text("Seleccionar");

            controls.txtOtherRelation.val("");

        },
        enableSolicitante: function () {
            var that = this,
                controls = that.getControls();
            controls.txtFirstName.prop("disabled", false);
            controls.txtLastName.prop("disabled", false);
            controls.txtDocumentNumber.prop("disabled", false);
            $("#cbDocumentType").prop("disabled", false);
            controls.txtOtherRelation.prop("disabled", false);
            controls.txtPhone.prop("disabled", false);
            controls.cbTitularRelation.prop("disabled", false);
            controls.txtParient.prop("disabled", false);
        },
        disableSolicitante: function () {
            var that = this,
                controls = that.getControls();
            controls.txtFirstName.prop("disabled", true);
            controls.txtLastName.prop("disabled", true);
            controls.txtDocumentNumber.prop("disabled", true);
            $("#cbDocumentType").prop("disabled", true);
            controls.txtPhone.prop("disabled", false);
            controls.txtParient.prop("disabled", false);
            controls.txtOtherRelation.prop("disabled", true);
            controls.cbTitularRelation.prop("disabled", true);
        },
        NmrdbSolicitante_click: function () {
            var that = this,
                controls = that.getControls();
            var value = $("input:radio[name=NmrdbSolicitante]:checked").val();
            if (value == "1") {
                that.disableSolicitante();
                that.setSolicitante();
                that.getDocumentType(false);
                controls.txtOtherRelation.hide();
                controls.cbTitularRelation.hide();
                controls.txtPhone.hide();
                controls.txtParient.hide();
                controls.lblTitularRelation.hide();
                controls.lblOtherRelation.hide();
                controls.lblPhone.hide();
                controls.lblParient.hide();

            } else if (value == "2") {

                that.cleanSolicitante();
                that.enableSolicitante();
                that.getDocumentType(false);
                controls.txtOtherRelation.show();
                controls.cbTitularRelation.show();
                controls.txtPhone.hide();
                controls.txtParient.hide();
                controls.lblTitularRelation.show();
                controls.lblOtherRelation.show();
                controls.lblPhone.hide();
                controls.lblParient.hide();

            } else if (value == "3" && that.flag20) {

                that.cleanSolicitante();
                that.enableSolicitante();
                that.getDocumentType(true);
                controls.txtOtherRelation.hide();
                controls.cbTitularRelation.hide();
                controls.txtPhone.show();
                controls.txtParient.show();
                controls.lblTitularRelation.hide();
                controls.lblOtherRelation.hide();
                controls.lblPhone.show();
                controls.lblParient.show();

            }

        },
        flag20: true,

        btnSearchIMEI_Click: function () {
            var that = this;
            var controls = that.getControls();
            controls.exampleModal.modal('show');

        },
        chkBloqueoEquipo_Click: function () {
            var that = this;
            var controls = that.getControls();
            if (controls.chkBloqueoEquipo.is(':checked')) {

                $("#idBloqueoEquipoPanel").show(1000);
                that.GetImeis();
            } else {
                $("#idBloqueoEquipoPanel").hide(1000);
            }
        },
        chkBloqueoLinea_Click: function () {
            var that = this;
            var controls = that.getControls();
            if (controls.chkBloqueoLinea.is(':checked')) {
                $("#idBloqueoLineaPanel").show(1000);
                that.GetCurrentLocks();
            } else {
                $("#idBloqueoLineaPanel").hide(1000);
            }
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
            that.loadCustomerData();
            that.getCACDAC();

            that.getDocumentType(false);
            that.getRelationParient();
            controls.txtOtherRelation.hide();
            controls.cbTitularRelation.hide();
            controls.txtPhone.hide();
            controls.txtParient.hide();
            controls.lblParient.hide();
            controls.lblTitularRelation.hide();
            controls.lblOtherRelation.hide();
            controls.lblPhone.hide();
            that.IniLoadPage();
            $("#cboTitular").prop("checked", true);
            $("#cboTitular").prop("disabled", false);
            $("#cboUsuario").prop("disabled", false);
            $("#cboRRLLCartaPoder").prop("disabled", false);
            that.validateDocument();
            that.NmrdbSolicitante_click();
            controls.btnConstancy.prop("disabled", true);
            $("#txtPhone").val("");
            $("#txtReferencePhone").val("");
            $("#txtNotes").val("");
            $("#txtImei").val("");
            $("#txtParient").val("");
            $("#idBloqueoLineaPanel").hide();
            $("#idBloqueoEquipoPanel").hide();
            controls.pnlPreguntas.hide();
            controls.chkBloqueoLinea.prop('checked', true);
            controls.chkBloqueoEquipo.prop('checked', false);
            that.chkBloqueoLinea_Click();
            that.chkBloqueoEquipo_Click();
            $("#btnSearchIMEI").hide();
            $(".clline").html(" <b>" + Session.DATACUSTOMER.Telephone + "</b>");
            controls.txtCorreo.val(Session.DATACUSTOMER.Email);
            that.ValidatechkEnvio();
            getRulesControls();
        },
        validateDocument: function () {
            var that = this,
               controls = that.getControls();
            var Tipo = Session.DATACUSTOMER.TipoDocumento;
            var NroDoc = Session.DATACUSTOMER.Nro_Doc;

            if (("RUC" == Tipo && NroDoc.match("^10")) || "DNI" == Tipo) {
                $("#txtDocumentNumber").val(NroDoc.substring(1, 10));

                $("#cboTitular").prop("checked", true);
                $("#cboTitular").prop("disabled", false);
                $("#cboUsuario").prop("disabled", false);
                $("#cboRRLLCartaPoder").prop("disabled", true);
            } if ("RUC" == Tipo && NroDoc.match("^20")) {
                $("#cboTitular").prop("checked", false);
                $("#cboTitular").prop("disabled", true);
                $("#cboUsuario").prop("checked", false);
                $("#cboUsuario").prop("disabled", true);
                $("#cboRRLLCartaPoder").prop("checked", true);
                $("#cboRRLLCartaPoder").prop("disabled", false);
                that.cleanSolicitante();
                that.disableSolicitante();
                that.getDocumentType(true);
                controls.txtOtherRelation.hide();
                controls.cbTitularRelation.hide();
                controls.txtPhone.show();
                controls.txtParient.show();
                controls.lblTitularRelation.hide();
                controls.lblOtherRelation.hide();
                controls.lblPhone.show();
                controls.lblParient.show();
                that.flag20 = false;
            }


        },
        GetImeis: function () {
            var that = this,
              controls = that.getControls(),
              objServiceLock = {};

            objServiceLock.strIdSession = Session.IDSESSION;
            objServiceLock.strLine = Session.DATACUSTOMER.Telephone;



            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objServiceLock),
                url: '/IFITransactions/ServiceLock/GetImeis',
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

                    if (response.data != null) that.lstTerminal = response.data;
                    that.FillTableEquipmentLock(response.data);


                }
            });
        },
        lstTerminal: null,
        FillTableEquipmentLock: function (data) {
            var that = this,
               controls = that.getControls();


            that.tblEquipmentLock = controls.tblEquipmentLock.DataTable({

                "info": false,

                "select": 'single',
                "paging": false,
                "searching": false,
                "destroy": true,

                "data": data,

                "columns": [
                    { "data": "_strCodigoBloqueo" },
                    { "data": "_strFechaMovimiento" },
                    { "data": "_strNumeroIMEI" },
                    { "data": "_strMarca" },
                    { "data": "_strEstado" },
                    { "data": "_strFechaRegistro" },
                    { "data": "_strTipoMovimiento" },
                    { "data": null },

                ],
                "columnDefs": [{
                    targets: 7,
                    render: function (data, type, full, meta) {
                        var $rb = $('<input>', {
                            type: 'radio',
                            value: full._strCodigoBloqueo,
                            name: 'optionsRadios',
                            id: full._strNumeroIMEI + "|" + full._strMarca + "|" + full._strTipoMovimiento
                        });
                        if (full._strEstado == "DESBLOQUEADO") $rb.attr('disabled', true);
                        return $rb[0].outerHTML;
                    }
                }]

                ,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "Sin resultados disponibles para la búsqueda.",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                },
            });
            $('input[name=optionsRadios]').change(function () {
                if ($('input[name=optionsRadios]').is(':checked')) {
                    if ($(this).attr('id') != null && $(this).attr('id') != "") {
                        var str = $(this).attr('id');
                        var strSplit = str.split("|");
                        controls.txtImei.val(strSplit[0]);

                        that.strMarca = strSplit[1];
                        that.strMov = strSplit[2];
                    }




                }
            });
        },
        strMarca: "",
        strMov: "",
        GetCurrentLocks: function () {
            var that = this,
               controls = that.getControls(),
               objServiceLock = {};

            objServiceLock.strIdSession = Session.IDSESSION;
            objServiceLock.codId = Session.DATACUSTOMER.ContratoID;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objServiceLock),
                url: '/IFITransactions/ServiceLock/CurrentBlock',
                complete: function () {

                    $.unblockUI();
                    that.ValidateStatusLine();
                    that.getTypeLock();
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
                    if (response.data.length > 0) {


                        $.each(that.CodeMotive, function (key, value) {

                            if (response.data[0]._Descripcion.indexOf(key) > 0) {

                                that.motivoLineDes = value;
                                return false;
                            }
                        });
                    }
                    if (response.data != null) that.lstLinesLocks = response.data;


                    that.FillTableLinesLock(response.data);


                }
            });
        },
        motivoLineDes: "-1",

        CodeMotivedt: { "Robo": "Robo", "Hurto": "Hurto", "Perdida": "Perdida", "Pérdida": "Pérdida" },
        lstLinesLocks: null,
        FillTableLinesLock: function (data) {
            var that = this,
               controls = that.getControls();


            that.tblLineLock = controls.tblLineLock.DataTable({

                "info": false,

                "select": 'single',
                "paging": false,
                "searching": false,
                "destroy": true,

                "data": data,

                "columns": [
                    { "data": "_Nro_Tickler" },
                    { "data": "_sFecha" },
                    { "data": "_Usuario_Seguimiento" },
                    { "data": "_desc_tipo" },
                    { "data": "_Descripcion" },


                ],
                "columnDefs": [
                {
                    "render": function (data, type, row) {
                        var xresult = data;
                        $.each(that.CodeMotivedt, function (key, value) {

                            if (row._Descripcion.indexOf(key) > 0) {

                                xresult = value;
                                return false;
                            }
                        });

                        return xresult;




                    },
                    "targets": 3
                }

                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "Sin resultados disponibles para la búsqueda.",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                },
            });

        },
        TestSecurity: function () {
            var that = this,
               controls = that.getControls();
            $.window.open({
                url: "/IFITransactions/ServiceLock/QuestionSecurity",
                type: 'POST',
                title: 'Preguntas Seguridad Call Center',
                modal: false,
                width: 864,
                height: 563,
                buttons: {
                    Confirmar: {
                        click: function (sender, args) {
                            if (that.listTest != null) {

                                var list = that.listTest;
                                var htmlTest = "";
                                $.each(list.paramListTest, function (key, value) {
                                    var idCbo = "#idCbo" + value.codQues + " option:selected";

                                    if ($.trim($(idCbo).text()) != "Seleccionar") {
                                        htmlTest += value.desQues + " - " + $(idCbo).text() + " \n";
                                    }
                                });
                                $("#txtNotesTest").html(htmlTest);
                            }
                            this.close();
                        }
                    },
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
            $('html,body').animate({
                scrollTop: $("body").offset().top
            }, 1000);
        },
        canLock: false,
        getDocumentType: function (isRll) {


            var that = this,
                controls = that.getControls(),
                objStateType = {};
            controls.cbDocumentType.empty();
            objStateType.strIdSession = Session.IDSESSION;
            objStateType.strIdList = "LISTA_DOCUMENTOS";

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objStateType),
                url: '/IFITransactions/ServiceLock/GetDocumentType',
                complete: function () {
                    that.setSolicitante();
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
                    controls.cbDocumentType.append($('<option>', { value: '', html: 'Seleccionar' }));

                    if (response.data != null) {
                        $.each(response.data.StateTypes, function (index, value) {
                            if (isRll == true) {
                                if (value.Description != "RUC") controls.cbDocumentType.append($('<option>', { value: value.Code, html: value.Description }));
                            } else {
                                controls.cbDocumentType.append($('<option>', { value: value.Code, html: value.Description }));
                            }

                        });
                    }
                }
            });
        },
        ValidateTypeTransaction: function () {
            var that = this;
            var controls = that.getControls();

            if (controls.chkBloqueoEquipo.is(':checked') && controls.chkBloqueoLinea.is(':checked')) {

                return "3";
            } else if (controls.chkBloqueoLinea.is(':checked')) {
                return "1";
            } else {
                return "2";
            }
        },
        huellapng: "",
        huellaminu: "",
        huellaCodeValidation: "",
        validateBiometric: function () {
            var that = thot;
		
            var controls = that.getControls();
            var url = "/IFITransactions/CommonServices/ShowBiometrics";

            var dd;
            if (controls.hdFlagBiometric.val() == "1") {
                that.UnlockService();
            } else {
                try {

                    var win = window.open(url, "BIOMETRIA", '_blank');

                    win.resizeTo(screen.width, screen.height);



                    win.moveTo(0, 0);

                    Session.responseBiometric = {};
                    Session.isclosed = false;
                    var cont0 = 0;
                    var timer0 = setInterval(function () {
                        if (typeof win != 'undefined' && win != null) {
                            cont0++;

                            if (win.opener) {
                                clearInterval(timer0);
                               




                            }
                        }

                    }, 1000);



                    var cont = 0;
                    var timer = setInterval(function () {

                        if (typeof win != 'undefined' && win != null) {
                            cont++;

                            if (win.closed || Session.isclosed) {


                                dd = JSON.parse(Session.responseBiometric);

                                if (typeof dd.RESPONSE_STATUS != 'undefined' && typeof dd.RESPONSE_STATUS != 'unknown') {


                                    if (dd.RESPONSE_STATUS.codeResponse === "0" || dd.RESPONSE_STATUS.codeResponse === 0) {

                                        that.huellapng = dd.RESPONSE_DATA.DATOS_PERSONA.HUELLA.HUELLA_PNG;
                                        that.huellaminu = dd.RESPONSE_DATA.DATOS_PERSONA.HUELLA.HUELLA_MINUCIAS;
                                        that.huellaCodeValidation = dd.RESPONSE_DATA.CODIGO_VALIDACION;
                                        that.UnlockService();
                                    }


                                    clearInterval(timer);
                                } else {


                                    clearInterval(timer);
                                }



                            }
                        } else {


                            clearInterval(timer);
                        }

                    }, 1000);
                }

                catch (e) {
                    alert("Hubo un problema con la biometria");
                    clearInterval(timer);
                }

            }


        },
        CodeMotive: { "Robo": "BLOQ_ROB1", "Hurto": "BLOQ_ROB2", "Perdida": "BLOQ_PER", "Pérdida": "BLOQ_PER" },
        UnlockService: function () {

            var that = this;
            var resu = false;
            var controls = that.getControls();

            try {

                var objParams = {};

                objParams.TipoTransaccion = that.ValidateTypeTransaction();
                objParams.msisdn = Session.DATACUSTOMER.Telephone;
                objParams.notes = $("#txtNotes").val();
                objParams.contractId = Session.DATACUSTOMER.ContratoID;
                objParams.idSession = Session.IDSESSION;
                objParams.nroDocumento = Session.DATACUSTOMER.Nro_Doc;

                objParams.LegalRep = Session.DATACUSTOMER.RepresentLegal;
                objParams.Solicitante = $("input:radio[name=NmrdbSolicitante]:checked").val();
                objParams.firstName = Session.CLIENTE.Name;
                objParams.LastName = Session.CLIENTE.LastName;
                objParams.StrTitularCliente = Session.CLIENTE.FullName;

                if (objParams.TipoTransaccion == "1") {



                    objParams.TipoBloqueo = $("#ddlMotive option:selected").text();
                    objParams.CodigoBloqueo = $("#ddlMotive option:selected").val();

                }
                console.log(333333);
                if (objParams.TipoTransaccion == "2") {


                    objParams.TipoBloqueoEquipo = $("#ddlMotive option:selected").text();
                    objParams.CodigoBloqueoEquipo = $("#ddlMotive option:selected").val();



                }
                console.log(4444);
                if (objParams.TipoTransaccion == "3") {



                    objParams.TipoBloqueo = $("#ddlMotive option:selected").text();
                    objParams.CodigoBloqueo = $("#ddlMotive option:selected").val();

                    objParams.TipoBloqueoEquipo = $("#ddlMotive option:selected").text();
                    objParams.CodigoBloqueoEquipo = $("#ddlMotive option:selected").val();


                }

                objParams.TelefonoReference = $("#txtReferencePhone").val() != "" ? $("#txtReferencePhone").val() : Session.CLIENTE.PhoneReference;
                if (objParams.TipoTransaccion == "2" || objParams.TipoTransaccion == "3") objParams.IMEI = controls.txtImei.val();

                objParams.Marca = that.strMarca;
                objParams.Email = $("#txtCorreo").val();
                objParams.EmailEnvio = controls.chkEnvio.is(':checked') ? "S" : "N";
                objParams.cac = $('#ddlCACDAC option:selected').text();
                objParams.strIdCac = $('#ddlCACDAC').val();

                objParams.cuenta = Session.CLIENTE.Account;
                objParams.TypeDocument = Session.DATACUSTOMER.TipoDocumento

                objParams.strSolicitanteTipoDoc = $("#cbDocumentType option:selected").text();
                objParams.strSolicitanteNombre = $("#txtFirstName").val();
                objParams.strSolicitanteApellido = $("#txtLastName").val();
                objParams.strSolicitanteNroDoc = $("#txtDocumentNumber").val();
                objParams.fullNameUser = Session.USERACCESS.NombreCompleto;
                if ($("input:radio[name=NmrdbSolicitante]:checked").val() == "3") {

                    objParams.strSolicitanteNro = $("#txtPhone").val();

                } else {
                    objParams.strSolicitanteNro = Session.DATACUSTOMER.Telephone;
                }


                objParams.currentUser = Session.ACCESO.login;

                //huella digital
                objParams.strHuellaTipDoc = $("#cbDocumentType option:selected").text();
                objParams.strHuellaNomApeCli = $("#txtFirstName").val() + " " + $("#txtLastName").val();
                objParams.strHuellaNroDoc = $("#txtDocumentNumber").val();
                objParams.strHuellaEncode = that.huellapng;
                objParams.strHuellaMinucia = that.huellaminu;
                objParams.strFirmaDigitalHuella = that.huellaCodeValidation == "01" ? "1" : "0";
                //huella digital

                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(objParams),
                    url: '/IFITransactions/UnlockService/SaveTransaction',
                    error: function (data) {
                        alert("Ha ocurrido un problema en la transacción, por favor volver a intentarlo más tarde.", "Alerta");
                        that.huellapng = "";
                        that.huellaminu = "";
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

                        if (response != null) {
                            controls.btnConstancy.prop("disabled", false);
                            alert(response.outMsg);
                            that.RutaArchivo = response.strPathFileHPxtream;
                            that.UpScroll();
                        }


                    },
                    complete: function () {
                        that.huellapng = "";
                        that.huellaminu = "";
                    }
                });

            } catch (e) {
                alert('Ocurrió un error al guardar en la transacción.', "Alerta");
            }

        },

        UpScroll: function () {
            $("html, body").animate({ scrollTop: "0px" }, 1000);

        },

        btnSave_click: function () {

            var that = this;
            var controls = that.getControls();
	thot=that;


            that.ValidateControl(function () {

            if ($('#formUnLock').valid()) {


                var tip = that.ValidateTypeTransaction();
                if (that.canLock) {
                    that.validateBiometric();
                } else {
                    if (tip == "2" || tip == "3") {

                        ValidateUserIFI('strKeyPerfDesBloquearServicioIFI', that.validateBiometric, null, null, null);
                    } else {
                        that.validateBiometric();
                    }
                }


            }




            });







        },
        ValidateControl: function (callback) {
            var that = this,
                control = that.getControls();

            if ($("#chkEnvio").prop("checked")) {
                if ($("#txtCorreo").val() == "") {
                    alert("Ingresar email", 'Alerta', function () {
                        control.txtCorreo.focus();
                    }); return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtCorreo").val());
                if (!blvalidar) {
                    alert("Ingresar email válido", 'Alerta', function () {
                        control.txtCorreo.select();
                    }); return false;
                }
            }
            var doc = $("#cbDocumentType option:selected").text();

            if (doc == "DNI") {
                if (!/^([0-9]{8})*$/.test($("#txtDocumentNumber").val())) {

                    alert("* Debe poner un formato correcto [8 digitos]");
                    return false;
                }
            }
            var doc2 = $("#cbDocumentType option:selected").text();
            if (doc2 == "CIE" || doc2 == "CTM" || doc2 == "CIRE") {


                if (!/^([a-zA-Z0-9]){4,12}$/.test($("#txtDocumentNumber").val())) {
                    console.log(2222);
                    alert("* Debe poner un formato correcto [alfanumérico como Mínimo 4 y máximo 12]");
                    return false;
                }
            }



            callback();

        },
        getTypeLock: function () {
            var that = this;
            var controls = that.getControls();
            var parameters = {};

            parameters.strIdSession = Session.IDSESSION;
            parameters.fileName = "IFIData.xml";
            parameters.strNameFunction = "ListaBloqueoLinea";
            parameters.strFlagCode = "1";
            var stroptionPermissions = Session.USERACCESS.optionPermissions;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: false,
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/ListValueXmlMethodIFI',
                success: function (response) {
                    controls.ddlMotive.html("");
                    controls.ddlMotive.append($('<option>', { value: '-1', html: 'Seleccionar' }));



                    if (response.data != null) {
                        $.each(response.data, function (index, value) {


                            controls.ddlMotive.append($('<option>', { value: value.Code, html: value.Description }));




                        });
                        $("#ddlMotive").val(that.motivoLineDes);
                    }
                }
            });
        },

        getCACDAC: function () {
            var that = this,
            objCacDacType = {
                strIdSession: Session.IDSESSION
            };

            var parameters = {};
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

        loadCustomerData: function () {
            var that = this;
            var controls = that.getControls();


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
                Segmento: Session.CLIENTE.CustomerType,
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

            //********** Datos del Cliente ***********/
            controls.lblContrato.html((Session.CLIENTE.ContractID == null) ? '' : Session.CLIENTE.ContractID);
            controls.lblCustomerID.html((Session.CLIENTE.CustomerID == null) ? '' : Session.CLIENTE.CustomerID);
            controls.lblTipoCliente.html((Session.CLIENTE.CustomerType == null) ? '' : Session.CLIENTE.CustomerType);
            controls.lblCliente.html((Session.CLIENTE.BusinessName == null) ? '' : Session.CLIENTE.BusinessName);
            controls.lblContacto.html((Session.CLIENTE.FullName == null) ? '' : Session.CLIENTE.FullName);
            controls.lblDNI_RUC.html((Session.CLIENTE.DNIRUC == null) ? '' : Session.CLIENTE.DNIRUC);
            controls.lblRepren_Legal.html((Session.CLIENTE.LegalAgent == null) ? '' : Session.CLIENTE.LegalAgent);
            controls.lblPlan.html((Session.DATALINEA.Plan == null) ? '' : Session.DATALINEA.Plan);
            controls.lblFechaActivacion.html((Session.DATACUSTOMER.FechaActivacion == null) ? '' : Session.DATACUSTOMER.FechaActivacion);
            controls.lblCicloFact.html((Session.CLIENTE.BillingCycle == null) ? '' : Session.CLIENTE.BillingCycle);
            controls.lblLimiteCred.html((Session.CLIENTE.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + Session.CLIENTE.objPostDataAccount.CreditLimit);
            //********** Direccíón de Facturación ***********/
            controls.lblDireccion.html((Session.DATACUSTOMER.Direccion_Despacho == null) ? '' : Session.DATACUSTOMER.Direccion_Despacho);
            controls.lblNotasDirec.html((Session.DATACUSTOMER.LegalUrbanization == null) ? '' : Session.DATACUSTOMER.LegalUrbanization);
            controls.lblPais.html((Session.DATACUSTOMER.Country == null) ? '' : Session.DATACUSTOMER.Country);
            controls.lblDepartamento.html((Session.DATACUSTOMER.Departament_Fact == null) ? '' : Session.DATACUSTOMER.Departament_Fact);
            controls.lblProvincia.html((Session.DATACUSTOMER.provincia == null) ? '' : Session.DATACUSTOMER.provincia);
            controls.lblDistrito.html((Session.DATACUSTOMER.District == null) ? '' : Session.DATACUSTOMER.District);
            controls.lblCodUbigeo.html((Session.CLIENTE.InstallUbigeo == null) ? '' : Session.CLIENTE.InstallUbigeo);
            $("#lblNroTelefono").html(Session.DATACUSTOMER.Telephone == null ? '' : Session.DATACUSTOMER.Telephone);

        },
        listTest: null,
        setListTest: function (paramListTest) {
            var that = this;
            that.listTest = paramListTest;
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

        maximizarWindow: function () {

            window.moveTo(0, 0);
            window.resizeTo(screen.width, screen.height);

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


        $.fn.UnlockServiceForm = function () {
            var option = arguments[0],
                args = arguments,
                value,
                 allowedMethods = ['setListTest'];

            this.each(function () {

                var $this = $(this),
                    data = $this.data('UnlockServiceForm'),
                    options = $.extend({}, $.fn.UnlockServiceForm.defaults,
                        $this.data(), typeof option === 'object' && option);

                if (!data) {
                    data = new Form($this, options);
                    $this.data('UnlockServiceForm', data);
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
    $.fn.UnlockServiceForm.defaults = {
    }
    $('#UnlockServiceForm').UnlockServiceForm();
})(jQuery);