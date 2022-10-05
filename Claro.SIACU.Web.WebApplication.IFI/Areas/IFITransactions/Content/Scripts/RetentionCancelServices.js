
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
    $("#txtDateCommitment").attr('disabled', false);
    $("#txtcalendartxtDateCommitment").attr('disabled', false);

};

var permisousu = "";
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTRetentionCancelServices.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , hidSupJef: $("#hidSupJef", $element)
            , cboCACDAC: $("#cboCACDAC", $element)
            , cboAccion: $("#cboAccion", $element)
            , cboMotCancelacion: $("#cboMotCancelacion", $element)
            , cboSubMotive: $("#cboSubMotive", $element)


            , txtTotInversion: $("#txtTotInversion", $element)
            , txtEmail: $("#txtEmail", $element)
            , txtPenalidad: $("#txtPenalidad", $element)
            , txtNote: $('#txtNote', $element)
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
            , rdbRetenido: $("#rdbRetenido", $element)
            , rdbNoRetenido: $("#rdbNoRetenido", $element)
            , rdbAplica: $("#rdbAplica", $element)
            , rdbNoAplica: $("#rdbNoAplica", $element)
            , chkEmail: $("#chkEmail", $element)
            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)

            , btnConstancia: $("#btnConstancia", $element)
            , btnValidar: $('#btnValidar', $element)
            , myModalLoad: $("#myModalLoad", $element)
            , divFlatRetencion: $("#divFlatRetencion", $element)
            , divCaso: $("#divCaso", $element)
            , divReglas: $("#divReglas", $element)
            , divSubMotive: $("#divSubMotive", $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , cboSchedule: $("#cboSchedule", $element)
            , txtDateCommitment: $("#txtDateCommitment", $element)
            , txtDateViewPartial: $("#txtDateViewPartial", $element)
            , txtcalendartxtDateCommitment: $("#txtcalendartxtDateCommitment", $element)


        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();
            controls.cboMotCancelacion.addEvent(that, 'change', that.cboMotCancelacion_change);


            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnGuardar.addEvent(that, 'click', that.SaveAfterValidateDate);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_click);
            controls.rdbRetenido.addEvent(that, 'click', that.rdbRetenido_click);
            controls.rdbNoRetenido.addEvent(that, 'click', that.rdbNoRetenido_click);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_Change);
            controls.txtDateCommitment.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateCommitment.addEvent(that, 'change', that.changeDateCommitment);
            controls.txtcalendartxtDateCommitment.addEvent(that, 'click', that.txtcalendartxtDateCommitment_click);

            
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        dblTotInversion:0.00,
        render: function () {

            var that = this,
            control = that.getControls();
            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);
            control.divErrorAlert.hide();
            that.hidAccionTra = 'R';
            control.rdbRetenido.attr("checked", true);
            control.divFlatRetencion.hide();
            control.divCaso.hide();
            $('tr.retenido').hide();
            control.txtPenalidad.attr("readonly", true);
            control.txtPenalidad.val("0.00");
            control.txtTotInversion.val("0.00");

            that.loadCustomerData();
            that.InitGetMessage();
            control.btnValidar.prop('disabled', true);
            control.btnValidar.hide();
            control.cboSchedule.hide();
            control.txtPenalidad.prop('disabled', true);
            control.chkEmail.attr("checked", true);
            if (control.chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
        },

        txtcalendartxtDateCommitment_click: function () {
            var that = this,
                control = that.getControls();

            if (document.getElementById("txtDateCommitment").disabled) {
                ValidateUserIFI('strKeyPerfActualizaFechaProgramacionINT', function () { document.getElementById("txtDateCommitment").disabled = false }, null, null, null);
            }
        },

        changeDateCommitment: function () {

            var that = this,
                controls = this.getControls();
            var DateString = controls.txtDateCommitment.val();

            var ano = DateString.substr(6, 4);
            var mes = DateString.substr(3, 2);
            var dia = DateString.substr(0, 2);
            var anoServ = that.FechaActualServidor.substr(0, 4);
            var mesServ = that.FechaActualServidor.substr(5, 2);
            var diaServ = that.FechaActualServidor.substr(8, 2);
            var fechaHoy = new Date(anoServ, mesServ, diaServ);
            var DateCommitment = new Date(ano, mes, dia);

            if (DateCommitment < fechaHoy) {
                alert("La fecha no puede ser menor que hoy");
                controls.txtDateCommitment.val('');

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
        },

        IniBegin: function () {
            var that = this,
            controls = this.getControls();

            that.IniLoadPage();
        },

        InitGetMessage: function () {
            var that = this,
                controls = this.getControls(),
                objModel = {};

            that.loadPage();
            Session.IDSESSION = "202201054784555";

            objModel.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: that.strUrlRetCan + '/GetMessage',
                success: function (response) {
                    if (response != null) {
                        that.gConstMsgLineaPOTP = response[0];
                        that.gConstMsgErrRecData = response[1];
                        that.hdnBajaTOTAL = response[2];
                        that.gConstFlagRetensionCancelacion = response[3];
                        that.strValueMotivoSOTDefecto = response[4];
                        that.strValueTipoTrabajoDefecto = response[5];
                        that.strFlagInhabTipTraMotSot = response[6];
                        that.gConstFlagRetensionCancelacionEstado = response[7];
                        that.gConstPerfHayCaso = response[8];
                        that.FechaActualServidor = response[9];
                        that.gConstMsgSelTr = response[10];
                        that.gConstMsgSelMot = response[11];
                        that.gConstMsgSelSubMot = response[12];
                        that.gConstMsgSelAc = response[13];
                        that.gConstMsgSelsinoCaso = response[14];
                        that.gConstMsgErrRecData = response[15];
                        that.gConstMsgErrCampNumeri = response[16];
                        that.gConstMsgSelCacDac = response[17];
                        that.gConstMensajeEsperaLoader = response[18];
                        that.CambNumSinCosto = response[19];
                        that.CostoCambioNumeroConsumer = response[20];
                        that.SusTempSinCostoReconexion = response[21];
                        that.MontoCobroReactivacionServicio = response[22];
                        that.strEstadoContratoInactivo = response[23];
                        that.strEstadoContratoReservado = response[24];
                        that.strMsgValidacionContratoInactivo = response[25];
                        that.strMsgValidacionContratoReservado = response[26];
                        that.flagRestringirAccesoTemporalCR = response[27];
                        that.gConstMsgOpcionTemporalmenteInhabilitada = response[28];
                        that.strMsgDebeCargLinea = response[29];
                        that.gConstMsgLineaStatSuspe = response[30];
                        that.strConsLineaDesaActiva = response[31];
                        that.strMessageValidationETA = response[32];
                        that.strMsgTranGrabSatis = response[33];
                        that.gConstCodigoTransRetCanServINT = response[34];
                        that.strMensajeRetenidoCancelacionDevolucionEquipoIFI = response[35];//PROY-140709_cambio_de_servicio- Evalenzs
                        
                        Session.VALIDATE = "0";
                        Session.COD_RESQUEST_ACT = "";
                        Session.CASE_ID = "";

                        that.IniValidateLoadPage();
                    }
                }
            });
        },

        IniValidateLoadPage: function () {
            this.GetTransactionScheduled();
        },

        IniLoadPage: function () {

            var that = this,
                controls = that.getControls(),

            objType = {};
            objType.strIdSession = Session.IDSESSION;
            objType.strContratoID = Session.DATACUSTOMER.ContratoID;
            objType.strListNumImportar = '';
            objType.strNroTelefono = Session.DATACUSTOMER.Telephone;
            objType.CadenaOpciones = Session.USERACCESS.optionPermissions;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: that.strUrlRetCan + '/IniLoadPage',
                async: false,
                error: function (data) {
                    alert("Error JS : en llamar al IniLoadPage.", "Alerta");
                },
                success: function (response) {

                    that.FechaREsultado = response.FechaResultado;
                    that.FlatReintegro = response.FlatReintegro;
                    that.hidFlatMsj = response.data;
                    that.PenalidadApadece = ((response.dblPenalidad == "" || response.dblPenalidad == undefined) ? "0.00" : response.dblPenalidad);
                    that.habilitaFecha = response.habilitaFecha;
                    permisousu = that.habilitaFecha;
                    that.Message = response.Message;

                    if (that.Message != "") {
                        controls.divErrorAlert.show();
                        controls.lblErrorMessage.text(that.Message);
                    }

                    controls.txtDateCommitment.prop("disabled", that.habilitaFecha);
                    controls.txtDateCommitment.val(that.FechaREsultado);

                    if (that.hidFlatMsj) {

                        that.isPostBackFlag = that.NumeracionCERO;

                        Session.DeshabilitaIndi = that.NumeracionCERO;
                        if (Session.TIPOSERVICIO = that.NumeracionUNO) {
                            that.f_AsignarApadece(that.NumeracionUNO);
                        } else {
                            that.f_AsignarApadece(that.NumeracionDOS);
                        }

                        that.hidCustomerID = Session.CUSTOMERID;
                        that.hidTelReferencia = Session.DATACUSTOMER.Telephone;
                        that.hidCuenta = Session.Cuenta;
                        that.hidCO = that.gConstKeyCoCanServ;

                        that.InitMotCancel();
                        that.InitAccion();

                        controls.txtPenalidad.val((that.Round(that.PenalidadApadece, 2)).toFixed(2));

                        if (that.FlatReintegro) {
                            controls.txtPenalidad.attr("readonly", false);
                        } else {
                            controls.txtPenalidad.attr("readonly", true);
                        }

                        that.DeterminaSiHayCaso();

                    } else {
                        alert('Error: Carga inicial Retención/Cancelación', "Alerta");
                    }

                },
            });
        },

        GetTransactionScheduled: function () {
            var that = this,
                control = that.getControls(),
                param = {};

            param.strIdSession = Session.IDSESSION;
            param.strContratoID = Session.DATACUSTOMER.ContratoID;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: that.strUrlRetCan + '/GetTransactionScheduled',
                error: function (response) {

                },
                success: function (response) {
                    if (response.data != null) {
                        if (Session.DATALINEA.StatusLinea == that.strEstadoContratoInactivo) {
                            alert(that.strMsgValidacionContratoInactivo, 'Alerta', function () { parent.window.close(); }); return;
                        } else if (Session.DATALINEA.StatusLinea == that.strEstadoContratoReservado) {
                            alert(that.strMsgValidacionContratoReservado, 'Alerta', function () { parent.window.close(); }); return;
                        }

                        if (that.flagRestringirAccesoTemporalCR == "1") {
                            alert(that.gConstMsgOpcionTemporalmenteInhabilitada, 'Alerta', function () { parent.window.close(); }); return;
                        }

                        //Validación Linea Activa
                        if (Session.DATALINEA.StatusLinea == that.strVariableEmpty) {
                            alert(that.strMsgDebeCargLinea, 'Alerta', function () { parent.window.close(); }); return;
                        } else if (Session.DATALINEA.StatusLinea == that.strConsLineaDesaActiva) {
                            alert(that.gConstMsgLineaStatSuspe, 'Alerta', function () { parent.window.close(); }); return;
                        }

                        //if (response.data == false) {
                        //    alert(that.gConstMsgLineaPOTP, 'Alerta', function () { parent.window.close(); }); return;
                        //}

                        that.IniBegin();
                        that.InitCacDaC();
                        that.getTypification();
                        that.mensajeInformativoDevoEquipo();
                    }
                }
            });
        },
        //PROY-140709_cambio_de_servicio-INICIO- Evalenzs
        mensajeInformativoDevoEquipo: function () {
            var that = this;
           var strMensaje = that.strMensajeRetenidoCancelacionDevolucionEquipoIFI;
            if (strMensaje != '')
                alert(strMensaje,'Aviso');

        },
        //PROY-140709_cambio_de_servicio-FIN- Evalenzs


        DeterminaSiHayCaso: function () {

            var that = this;
            that.hayCaso = "1";

        },

        CalculaDiasHabiles: function () {

            var that = this,
                controls = that.getControls(),
            objType = {};
            objType.NroDias = '8';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: that.strUrlRetCan + '/CalculaDiasHabiles',
                async: false,

                error: function (data) {
                    alert("Error JS : en llamar al CalculaDiasHabiles.", "Alerta");
                },
                success: function (response) {
                    FechaInicia = response.data;

                },

            });

        },

        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();
            controls.lblTitle.text("Retención / Cancelación de Servicio");
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
            //Constantes
            that.strVariableEmpty = "";
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
            controls.txtEmail.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);

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

        cboMotCancelacion_change: function () {
            var that = this,
           controls = that.getControls(),
                param = {};

            that.hdnMotivo = $('#cboMotCancelacion option:selected').text();
            param.strIdSession = Session.IDSESSION;
            param.IdMotive = controls.cboMotCancelacion.val();

            console.log("Va limpiar el cboAccion");
            controls.cboAccion.val("");


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: that.strUrlRetCan + '/GetSubMotiveCancel',
                success: function (response) {
                    controls.cboSubMotive.html("");
                    controls.cboSubMotive.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        if (response.data.length > 0) {
                            $.each(response.data, function (index, value) {
                                controls.cboSubMotive.append($('<option>', { value: value.Code, html: value.Description }));
                            });

                            controls.divSubMotive.show();
                            that.hdnHaySubM = "1";
                        } else {
                            controls.divSubMotive.hide();
                            that.hdnHaySubM = "-1";
                        }
                    } else {
                        controls.divSubMotive.hide();
                        that.hdnHaySubM = "-1";
                    }
                },
                error: function (response) {
                    alert(that.gConstMsgErrRecData, "Alerta");
                }
            });
        },

        cboSubMotive_change: function () {
            that.hdnSubMot = ($('#cboSubMotive').val());
            that.hdnSubMotDesc = $('#cboSubMotive option:selected').text();
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

            var that = this,
              controls = this.getControls();
            
            that.isPostBackFlag = that.NumeracionUNO;

            //
            if (!that.VarlidateControl()) {
                return false;
            }
            //

            //if (permisousu == true) {

            //    ValidateUserIFI('strKeyPerfActualizaFechaProgramacionINT', function () {
                    confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {
                        console.log(that.hidAccionTra);

                        //PROY-140709_cambio_de_servicio-INICIO- Evalenzs
                        //if (!that.mensajeInformativoDevoEquipo()) {
                        //    return false;
                        //}
                        //PROY-140709_cambio_de_servicio-FIN- Evalenzs

                        if (that.hidAccionTra == 'R') {

                            that.Loading();

                            that.SaveTransactionRetention();

                        } else {
                            that.Loading();
                            that.SaveTransactionNoRetention();
                        }
                    }, function () {
                        $("#hidAccion").val("");
                        $.unblockUI();
                        return false;
                       
                    });
                //}, null, null, null);


            //} else {
            //    that.validateUser();

            //}


        },

        SaveAfterValidateDate: function () {
            var that = this,
                result = false,
                controls = that.getControls();
            var DateString = controls.txtDateCommitment.val();

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;

            console.log(today);
            console.log(DateString);

            console.log(DateString === today);
            if (DateString === today) {
                confirm("La cancelacion se realizara el dia de hoy.", 'Confirmar', function () {

                    console.log("confima");
                    that.btnGuardar_click();

                }, function () {
                    console.log("no confima");
                    return false;
                });
            } else {
                that.btnGuardar_click();
            }
        },

        validateUser: function () {
            console.log(5555);
            var that = this;
            console.log(that);
            confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {
                console.log(that.hidAccionTra);
                //PROY-140709_cambio_de_servicio-INICIO- Evalenzs
                //if (!that.mensajeInformativoDevoEquipo()) {
                //    return false;
                //}
                //PROY-140709_cambio_de_servicio-FIN- Evalenzs
                if (that.hidAccionTra == 'R') {

                    that.Loading();
                    that.SaveTransactionRetention();
                } else {
                    that.SaveTransactionNoRetention();
                    that.Loading();

                }
            }, function () {
                $("#hidAccion").val("");
                return false;
            });
        },
        VarlidateControl: function () {
            var that = this,
                control = that.getControls();

            var getestadopermiso = "";
            var res = false;
            getestadopermiso = permisousu;


            if (control.rdbNoRetenido[0].checked || control.rdbRetenido[0].checked) {

            } else {
                alert("Seleccione tipo de Servicio.", "Alerta");
                return false;
            }

            if (control.cboMotCancelacion.val() == "-1" || control.cboMotCancelacion.val() == "") {
                alert("Seleccione Motivo de Cancelación.", "Alerta");
                return false;
            }
            if ($("#cboSubMotive option").length > 1) {
                if (control.cboSubMotive.val() == "-1" || control.cboSubMotive.val() == "") {
                    alert("Seleccione un SubMotivo de Cancelación.", "Alerta");
                    return false;
                }
            }

            if (control.cboAccion.val() == "-1" || control.cboAccion.val() == "") {
                alert("Seleccione una Acción.", "Alerta");
                return false;
            }



            if ($.isNumeric(control.txtPenalidad.val()) && $.isNumeric(control.txtTotInversion.val())) {

            } else {
                alert("ingrese un valor numérico.", "Alerta");
                return false;
            }

            if (control.rdbNoRetenido[0].checked) {

                if (control.txtDateCommitment.val() == "-1" || control.txtDateCommitment.val() == "") {
                    alert("Seleccione una fecha de programación.", "Alerta");
                    return false;
                }



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


            if (control.cboCACDAC.val() == "-1" || control.cboCACDAC.val() == "") {
                alert("Seleccione Punto de Atención.", "Alerta");
                return false;
            }


            if (control.rdbNoRetenido[0].checked) {
                if (!(control.rdbAplica[0].checked || control.rdbNoAplica[0].checked)) {
                    alert('Seleccione "SI" o "NO" aplica Caso.', "Alerta");
                    return false;
                }
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

        rdbRetenido_click: function () {
            var that = this,
                control = that.getControls();

            that.hidAccionTra = 'R';
            $('tr.retenido').hide();
            control.divFlatRetencion.hide();
            control.divCaso.hide();
            control.rdbAplica.attr("checked", false);
            control.rdbNoAplica.attr("checked", true);
            control.txtPenalidad.attr("readonly", true);
            control.txtPenalidad.val(that.PenalidadApadece);
            control.txtPenalidad.prop('disabled', true);
            control.txtDateCommitment.val('');
        },

        rdbNoRetenido_click: function () {
            var that = this,
                control = that.getControls();

            that.hidAccionTra = 'NR';
            $('tr.retenido').show();
            control.divFlatRetencion.show();
            control.txtPenalidad.attr("readonly", false);
            control.rdbAplica.attr("checked", false);
            control.rdbNoAplica.attr("checked", true);
            control.txtPenalidad.val(that.PenalidadApadece);
            control.txtPenalidad.prop('disabled', true);
            that.DetermineCase();
            control.divCaso.hide();

        },

        f_AsignarApadece: function (obj) {

            //
            if (obj == "1") {
                $("#idTDApadeceDes").val("Reintegro APADECE:");
            } else {
                $("#idTDApadeceDes").val("Reintegro APALECE:");
            }

        },

        IniGetParameter: function (gConstDiasHabiles) {

            var that = this,
            controls = that.getControls(),
            objType = {};
            objType.strIdSession = Session.IDSESSION;
            objType.name = gConstDiasHabiles;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: '/IFITransactions/CommonServices/GetParameterData',
                async: false,

                error: function (data) {
                    alert("Error JS : en llamar al GetParameterData.", "Alerta");
                },
                success: function (response) {
                    Value_C = response.data.Parameter.Value_C;

                },

            });

        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },



        DetermineCase: function () {
            var that = this,
                control = that.getControls();

            if (that.hayCaso == "1") {
                control.divCaso.show();
            } else {
                control.divCaso.hide();
            }
        },



        BlockControl: function () {
            var that = this,
            control = that.getControls();

            control.txtPenalidad.prop('disabled', true);
            control.cboMotCancelacion.prop('disabled', true);
            control.cboSubMotive.prop('disabled', true);
            control.cboAccion.prop('disabled', true);


            control.cboSchedule.prop('disabled', true);
            control.cboCACDAC.prop('disabled', true);
            control.txtNote.prop('disabled', true);
            control.divCaso.prop('disabled', true);
            control.chkEmail.prop('disabled', true);
            control.rdbNoRetenido.prop('disabled', true);
            control.rdbRetenido.prop('disabled', true);
        },
        

        SaveTransactionRetention: function () {
            var that = this,
                controls = that.getControls(),
                strUrl = '',
                model = {};

            model.IdSession = Session.IDSESSION;
            model.fullNameUser = Session.USERACCESS.NombreCompleto;
            model.Reintegro = $("#txtPenalidad").val();
            model.DesMotivos = $('#cboMotCancelacion option:selected').text();
            model.DesAccion = $('#cboAccion option:selected').text();
            model.hidSupJef = that.hidSupJef;
            model.NroCelular = Session.DATACUSTOMER.Telephone;
            model.Accion = that.hidAccionTra;
            model.DesSubMotivo = $('#cboSubMotive option:selected').text();
            model.DescCacDac = $('#cboCACDAC option:selected').text();
            model.TotalInversion = $("#txtTotInversion").val();
            model.PagoAPADECE = that.hidPagoAPADECE
            model.NameComplet = Session.DATACUSTOMER.NameCompleto;
            model.NroDoc = Session.DATACUSTOMER.Nro_Doc;
            model.RazonSocial = Session.DATACUSTOMER.BusinessName;
            model.RepresentLegal = Session.DATACUSTOMER.RepresentLegal;
            model.DNI_RUC = Session.DATACUSTOMER.DNI_RUC;
            model.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
            model.TelefonoReferencia = Session.DATACUSTOMER.telefono_Referencial;
            model.CodTypeClient = Session.DATACUSTOMER.CustomerTypeCode;
            model.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            model.Reference = Session.DATACUSTOMER.refencial;
            model.Departament_Fact = Session.DATACUSTOMER.Departament_Fact;
            model.District_Fac = Session.DATACUSTOMER.District_Fac;
            model.Pais_Fac = Session.DATACUSTOMER.Country_Fac;
            model.Provincia_Fac = Session.DATACUSTOMER.Provincia_Fac;
            model.ContractId = Session.DATACUSTOMER.ContratoID;
            model.TypeClient = Session.DATACUSTOMER.TipoCliente;
            model.CustomerId = Session.DATACUSTOMER.CustomerID;
            model.Cuenta = Session.DATACUSTOMER.Cuenta;
            model.Note = $("#txtNote").val();
            model.Code_Plane_Inst = Session.DATACUSTOMER.Codigo_Plano_Inst;
            model.CurrentUser = Session.USERACCESS.Login;
            model.Destinatarios = $("#txtEmail").val();
            model.fechaActual = that.FechaActualServidor;
            model.Email = $("#txtEmail").val();

            if (controls.chkEmail[0].checked) {
                model.Flag_Email = 'true';
            } else {
                model.Flag_Email = 'false';
            }
            model.Sn = that.hdnServName;
            model.IpServidor = that.hdnLocalAdd;
            model.Telephone = Session.DATACUSTOMER.Telephone;
            model.Agent = Session.ACCESO.fullName;
            model.Segmento = Session.DATACUSTOMER.Segmento === 'Consumer' ? 'MASIVO' : 'CORPORATIVO';

            strUrl = that.strUrlRetCan + '/SaveTransactionRetention';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: strUrl,
                error: function (data) {
                    alert(that.strMensajeDeError, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);

                    $.unblockUI();

                },
                success: function (response) {
                    that.MensajeEmail = response.MensajeEmail;
                    that.vDesInteraction = response.vDesInteraction;
                    that.InteractionId = response.vInteractionId;
                    that.RutaArchivo = response.strRutaArchivo;

                    if (that.InteractionId != '' || that.InteractionId != "null") {
                        $("#btnConstancia").attr('disabled', false); //Activa                        
                        alert(that.vDesInteraction, "Informativo");

                        that.BlockControl();
                        $("#btnGuardar").attr('disabled', true);
                    }
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },

        SaveTransactionNoRetention: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                objRegister = {},
                objSession = {};



            objRegister.IdSession = Session.IDSESSION;
            objRegister.CustomerId = Session.DATACUSTOMER.CustomerID;
            objRegister.AreaPCs = that.NumeracionMENOSUNO;
            objRegister.DescCacDac = $('#cboCACDAC option:selected').text();
            objRegister.BillingCycle = Session.DATACUSTOMER.Ciclo_Facturacion;
            objRegister.ContractId = Session.DATACUSTOMER.ContratoID;
            objRegister.Account = Session.DATACUSTOMER.Cuenta;
            objRegister.CodigoInteraction = '';

            objRegister.fechaActual = that.FechaActualServidor;
            objRegister.FechaProgramacion = controls.txtDateCommitment.val();
            objRegister.DateProgrammingSot = controls.txtDateCommitment.val();
            objRegister.FlagNdPcs = that.NumeracionCERO;
            objRegister.TotalInversion = $("#txtTotInversion").val();
            objRegister.Email = $("#txtEmail").val();
            objRegister.MontoFidelizacion = that.NumeracionCERO;
            objRegister.MontoPCs = that.NumeracionCERO;
            objRegister.montoPenalidad = that.NumeracionMENOSUNO;
            objRegister.MotivePCS = that.NumeracionMENOSUNO;
            objRegister.Msisdn = Session.DATACUSTOMER.Telephone;
            objRegister.DocumentNumber = Session.DATACUSTOMER.Nro_Doc;
            objRegister.Observation = $("#txtNote").val();
            objRegister.SubMotivePCS = that.NumeracionMENOSUNO;
            objRegister.TypeClient = Session.DATACUSTOMER.TipoCliente;

            objRegister.Trace = that.NumeracionUNO;
            objRegister.CurrentUser = Session.USERACCESS.Login;
            objRegister.Reintegro = $("#txtPenalidad").val();
            objRegister.DesMotivos = $('#cboMotCancelacion option:selected').text();
            objRegister.DesAccion = $('#cboAccion option:selected').text();
            objRegister.hidSupJef = that.hidSupJef;
            objRegister.NroCelular = Session.DATACUSTOMER.Telephone;
            objRegister.Accion = that.hidAccionTra;
            objRegister.DesSubMotivo = $('#cboSubMotive option:selected').text();
            objRegister.PagoAPADECE = that.hidPagoAPADECE
            objRegister.NameComplet = Session.DATACUSTOMER.NameCompleto;
            objRegister.RazonSocial = Session.DATACUSTOMER.BusinessName;
            objRegister.RepresentLegal = Session.DATACUSTOMER.RepresentLegal;
            objRegister.DNI_RUC = Session.DATACUSTOMER.DNI_RUC;
            objRegister.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
            objRegister.TelefonoReferencia = Session.DATACUSTOMER.telefono_Referencial;
            objRegister.CodTypeClient = Session.DATACUSTOMER.CustomerTypeCode;
            objRegister.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            objRegister.Reference = Session.DATACUSTOMER.refencial;
            objRegister.Departament_Fact = Session.DATACUSTOMER.Departament_Fact;
            objRegister.District_Fac = Session.DATACUSTOMER.District_Fac;
            objRegister.Pais_Fac = Session.DATACUSTOMER.Country_Fac;
            objRegister.Provincia_Fac = Session.DATACUSTOMER.Provincia_Fac;
            objRegister.CodePlanInst = Session.DATACUSTOMER.Codigo_Plano_Inst;
            objRegister.Sn = that.hdnServName;
            objRegister.IpServidor = that.hdnLocalAdd;
            objRegister.Telephone = Session.DATACUSTOMER.Telephone;
            objRegister.CurrentUser = Session.USERACCESS.Login;
            objRegister.TotInversion = $("#txtTotInversion").val();
            if (controls.rdbAplica.prop('checked')) {
                objRegister.Aplica = 'Si'
            } else {
                objRegister.Aplica = 'No'
            }
            objRegister.Destinatarios = $("#txtEmail").val();

            objRegister.Note = $("#txtNote").val();

            if (controls.chkEmail[0].checked) {
                objRegister.Flag_Email = 'true';
            } else {
                objRegister.Flag_Email = 'false';
            }


            objRegister.Agent = Session.ACCESO.fullName;
            objRegister.FechaCompromiso = controls.txtDateCommitment.val();
            objRegister.vSchedule = '';
            objRegister.Segmento = Session.DATACUSTOMER.Segmento === 'Consumer' ? 'MASIVO' : 'CORPORATIVO';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRegister),
                url: that.strUrlRetCan + '/SaveTransactionNoRetention',
                success: function (response) {

                    that.MensajeEmail = response.data.Description;
                    that.InteractionId = response.data.Condition;
                    that.Message = response.mssj;
                    that.RutaArchivo = response.data.Description2;

                    if (response.data.Code == 'TRUE') {
                        $("#btnConstancia").attr('disabled', false); //Activa                      
                        alert(that.strMsgTranGrabSatis, "Informativo");
                    }
                    else {
                        alert(that.Message, "Alerta");
                        $("#btnConstancia").attr('disabled', true);  //Desactiva
                    }

                    that.BlockControl();
                    $("#btnGuardar").attr('disabled', true);

                },
                complete: function () {
                    $.unblockUI();
                },
                error: function () {
                    alert(that.strMensajeDeError, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);  //Desactiva
                    $.unblockUI();
                }
            });
        },

        InitAccion: function () {

            var that = this,
           controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: that.strUrlRetCan + '/GetListarAccionesRC',
                success: function (response) {
                    controls.cboAccion.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboAccion.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });

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

        InitMotCancel: function () {
            var that = this,
            controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: that.strUrlRetCan + '/GetMotCancelacion',
                success: function (response) {
                    controls.cboMotCancelacion.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMotCancelacion.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },





        getTypification: function () {
            var that = this, controls = that.getControls();
            var transact = that.gConstCodigoTransRetCanServINT;

            $.app.ajax({
                type: "POST",
                url: "/IFITransactions/CommonServices/GetTypification",
                data: {
                    strIdSession: Session.IDSESSION,
                    strTransactionName: transact
                },
                success: function (result) {
                    var list = result.ListTypification;
                    if (result.ListTypification.length > 0) {
                        that.getBusinessRules(list[0].SUBCLASE_CODE);
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

        intContratoID: '',
        NumeracionMENOSUNO: '-1',
        NumeracionUNO: '1',
        NumeracionDOS: '2',
        NumeracionCERO: '0',
        Value_C: '',
        gConstCodigoTransRetCanServINT: "",
        strUrlRetCan: '/IFITransactions/RetentionCancelServices',
        hidAccionTra: "",
        FlatReintegro: "",
        hayCaso: "1",
        hdnSubMot: "",
        hdnSubMotDesc: "",
        InteractionId: "",
        AccesPage: "1",
        //----------------------------
        hFlatInteraccion: "",
        hdnGeneroCaso: "",
        hidDeshabilitaIndi: "",
        hidenMotivoSot: "",
        hdnSubTipOrdCU: "",
        hdnValidaEta: "",
        hdnTipoTrabCU: "",
        hidClaseId: "",
        hidSubClaseId: "",
        hidTipo: "",
        hidClaseDes: "",
        hidSubClaseDes: "",
        hdnFecAgCU: "",
        hidFechValida: "",
        hidFlatMsj: "",
        FechaREsultado: "",
        PenalidadApadece: "",
        hidSupJef: "",
        hidPagoAPADECE: "",
        Message: "",
        GeneroCaso: "",
        hdnServName: "",
        hdnLocalAdd: "",
        UserHostName: "",
        RutaArchivo: "",
        Filename: "",
        //Mensajes
        strMsgDebeCargLinea: "",
        flagRestringirAccesoTemporalCR: "",
        gConstMsgOpcionTemporalmenteInhabilitada: "",
        strEstadoContratoInactivo: "",
        strEstadoContratoReservado: "",
        strMsgValidacionContratoInactivo: "",
        strMsgValidacionContratoReservado: "",
        CambNumSinCosto: "",
        CostoCambioNumeroConsumer: "",
        SusTempSinCostoReconexion: "",
        MontoCobroReactivacionServicio: "",
        gConstMsgSelTr: "",
        gConstMsgSelMot: "",
        gConstMsgSelSubMot: "",
        gConstMsgSelAc: "",
        gConstMsgSelsinoCaso: "",
        gConstMsgErrRecData: "",
        gConstMsgErrCampNumeri: "",
        gConstMsgSelCacDac: "",
        gConstMensajeEsperaLoader: "",

        gConstPerfHayCaso: "",
        gConstFlagRetensionCancelacionEstado: "",
        strFlagInhabTipTraMotSot: "",
        strValueMotivoSOTDefecto: "",
        strValueTipoTrabajoDefecto: "",
        gConstFlagRetensionCancelacion: "",
        hdnBajaTOTAL: "",
        hdnValidado: "",
        hdnFranjaHorariaCU: "",
        hdnHaySubM: "",
        strConsLineaDesaActiva: "",
        strMessageValidationETA: "",
        gConstMsgLineaPOTP: "",
        strMensajeDeError: "No se pudo ejecutar la transacción. Informe o vuelva a intentar.",
        strMsgTranGrabSatis: "",
        isPostBackFlag: "",
        gconstvalidation: '',
        gConstKeyCoCanServ: '10355', //gConstKeyCoCanServ
        strMensajeRetenidoCancelacionDevolucionEquipoIFI: '',//PROY-140709_cambio_de_servicio- Evalenzs
       
    };

    $('#txtTotInversion').on('keypress', function (e) {
        //
        var regexp = "";
        var field = $(this);
        var key = e.keyCode ? e.keyCode : e.which;

        if (key == 8) return true;
        if (key > 47 && key < 58) {
            if (document.getSelection() == field.val()) field.val("");
            if (field.val() === "") return true;
            var existePto = (/[.]/).test(field.val());
            if (existePto === false) {
                regexp = /.[0-9]{10}$/; //PARTE ENTERA
            }
            else {
                regexp = /.[0-9]{2}$/; //PARTE DECIMAL 2
            }
            return !(regexp.test(field.val()));
        }
        if (key == 46) {
            if (field.val() === "") return false;
            regexp = /^[0-9]+$/;
            return regexp.test(field.val());
        }
        return false;
    });

    $('#txtPenalidad').on('keypress', function (e) {
        //
        var regexp = "";
        var field = $(this);
        var key = e.keyCode ? e.keyCode : e.which;

        if (key == 8) return true;
        if (key > 47 && key < 58) {
            if (document.getSelection() == field.val()) field.val("");
            if (field.val() === "") return true;
            var existePto = (/[.]/).test(field.val());
            if (existePto === false) {
                regexp = /.[0-9]{10}$/; //PARTE ENTERA
            }
            else {
                regexp = /.[0-9]{2}$/; //PARTE DECIMAL 2
            }
            return !(regexp.test(field.val()));
        }
        if (key == 46) {
            if (field.val() === "") return false;
            regexp = /^[0-9]+$/;
            return regexp.test(field.val());
        }
        return false;
    });



    $.fn.INTRetentionCancelServices = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTRetentionCancelServices'),
                options = $.extend({}, $.fn.INTRetentionCancelServices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTRetentionCancelServices', data);
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

    $.fn.INTRetentionCancelServices.defaults = {
    }

    $('#divBody').INTRetentionCancelServices();
})(jQuery);