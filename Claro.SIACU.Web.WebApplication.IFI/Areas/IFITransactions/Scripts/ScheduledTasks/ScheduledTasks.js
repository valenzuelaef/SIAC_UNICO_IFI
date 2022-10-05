//Variables para tipificacion
var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};
var DATACUSTOMER = {};
var DATASERVICE = {};
var USERACCESS = {};
var URLPARAMS = {};
var IDSESSION = 0;
//Numero de Linea
var MSISDN = "";
//HPXTREAM
var PATH_FILE_HPXTREAM = "";
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.ScheduledTasksForm.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , txtContract: $('#txtContract', $element)
            , txtAccount: $('#txtAccount', $element)
            , txtPhoneNumber: $('#txtPhoneNumber', $element)
            , ddlStatus: $('#ddlStatus', $element)
            , txtDateStart: $('#txtDateProgStart', $element)
            , txtDateEnd: $('#txtDateProgEnd', $element)
            , ddlTypeTransaction: $('#ddlTypeTransaction', $element)
            , txtCodeTransaction: $('#txtCodeTransaction', $element)
            , txtAdvisor: $('#txtAdvisor', $element)
            , ddlCacDac: $('#ddlCacDac', $element)
            , tblScheduledTask: $('#tblScheduledTask', $element)
            , btnSearch: $('#btnSearch', $element)
            , btnExport: $('#btnExport', $element)
            , btnClose: $('#btnClose', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)

        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            controls.btnSearch.addEvent(that, 'click', that.btnSearch_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btnExport.addEvent(that, 'click', that.btnExportToExcel_click);
            controls.btnExport.attr('disabled', 'disabled');

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        cacdac: "", tiptrandelete: "",
        render: function () {
            var that = this;
            var controls = that.getControls();
            that.loadCustomerData();
            that.setInitValues();
            that.getStateType();
            that.getTransactionType();
            that.getCacDacType();
            that.getCACDAC();

        },
        getLoadTypification: function (objTypification) {

            TYPIFICATION.ClaseId = objTypification.CLASE_CODE;
            TYPIFICATION.SubClaseId = objTypification.SUBCLASE_CODE;
            TYPIFICATION.Tipo = objTypification.TIPO;
            TYPIFICATION.ClaseDes = objTypification.CLASE;
            TYPIFICATION.SubClaseDes = objTypification.SUBCLASE;
            TYPIFICATION.TipoId = objTypification.TIPO_CODE;
        },
        getTipification: function () {

            var that = this;
            controls = that.getControls();
            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idSession: Session.IDSESSION }),
                url: '/IFITransactions/SuspensionReconnectionCustomer/GetTypificationDelete',
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


                    if (response.typificationLoad.result) {
                        if (response.typificationLoad.objResult != null) {
                            that.getLoadTypification(response.typificationLoad.objResult);
                        }

                    } else {
                        controls.divErrorAlert.show();
                        controls.lblErrorMessage.html(response.typificationLoad.lblMensaje);

                    }



                }
            });
        },
        btnSearch_click: function (e) {
            var that = this,
                controls = that.getControls();
            if (typeof e != "undefined") that.IsDelete = false;
            var oScheduledTasksRequest = {};


            oScheduledTasksRequest = {
                ServiCoId: controls.txtContract.val(),
                Account: controls.txtAccount.val(),
                StrStartDate: controls.txtDateStart.val(),
                StrEndDate: controls.txtDateEnd.val(),
                ServiceState: controls.ddlStatus.val(),
                Advisor: controls.txtAdvisor.val(),
                TransactionType: controls.ddlTypeTransaction.val(),
                CodeInteraction: controls.txtCodeTransaction.val(),
                NameCACDAC: controls.ddlCacDac.val(),
                strIdSession: Session.IDSESSION
            };

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(oScheduledTasksRequest),
                url: '/IFITransactions/ScheduledTasks/GetScheduledTransaction',
                complete: function () {
                    $.unblockUI();
                    that.linkEdit_Click();
                    that.linkDelete_Click();
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
                    controls.tblScheduledTask.find('tbody').html('');
                    if (response.data.ScheduledTransactions != null) {

                        that.loadTableScheduledTasks(response);
                        if (response.data.ScheduledTransactions.length > 0) {
                            controls.btnExport.removeAttr('disabled');
                        }
                        else {
                            if (!that.IsDelete) alert("No hay transacciones programadas para esta búsqueda", "Alerta");

                            controls.btnExport.attr('disabled', 'disabled');
                        }
                    }
                    else {
                        controls.tblScheduledTask.find('tbody').html('<tr class="odd"><td align="center" valign="top" colspan="10" class="dataTables_empty">Sin resultados disponibles para la búsqueda.</td></tr>');
                        controls.btnExport.attr('disabled', 'disabled');
                        if (!that.IsDelete) alert("No hay transacciones programadas para esta búsqueda", "Alerta");
                    }
                },
                error: function (exception) {
                    alert("Error al ejecutar solicitud, favor de intentar en unos minutos.", "Alerta")
                },
            });
        },
        IsDelete: false,
        loadTableScheduledTasks: function (data) {
            var that = this,
                controls = that.getControls();



            that.tblScheduledTasks = controls.tblScheduledTask.DataTable({
                "scrollCollapse": true,
                "info": false,
                "scrollY": "300px",
                "select": 'single',
                "paging": false,
                "searching": false,
                "destroy": true,
                "data": data.data.ScheduledTransactions,

                "columns": [
                    { "data": "CoId" },
                    { "data": "CustomerId" },
                    { "data": "ServdDateProg", type: 'date-dd-mmm-yyyy' },
                    { "data": "ServdDateReg" },
                    { "data": "ServdDateEjec" },
                    { "data": "DescState" },
                    { "data": "DescServi" },
                    { "data": "ServcNumberAccount" },
                    { "data": "ServcTypeServ" },
                    { "data": null },
                    { "data": "ServcCodeInteraction" }
                ],
                "order": [[2, "desc"]],
                columnDefs: [{
                    "targets": [9],
                    "render": function (data, type, full, meta) {
                        var actionshtml = '';

                        if (full.ServcState != '3' && full.ServcState != '4') {
                            if (full.DescServi != 'REACTIVACION') {
                                if (full.ServcCoSer == '1236') {
                                    if (full.ServcState == '1') {
                                        actionshtml += '<a class="deleteTasks" href="#">Borrar Prog.</a>';
                                    }
                                } else {
                                    if (full.ServcState != '2') {
                                    actionshtml += '<a class="deleteTasks" href="#">Borrar Prog.</a>';
                                }
                            }
                            }
                            if (full.DescServi == 'REACTIVACION' || full.DescServi == 'SUSPENSION') {
                                if (actionshtml != '') {
                                    actionshtml += '/<a class="editTasks" id=' + full.ServcCodeInteraction + ' href="#">Editar Prog.</a>';
                                } else {
                                    actionshtml += '<a class="editTasks" id=' + full.ServcCodeInteraction + ' href="#">Editar Prog.</a>';
                                }
                            }
                        } else
                            actionshtml += '-';

                        return actionshtml;
                    }
                },
                { visible: false, targets: 10, width: 0 }],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "Sin resultados disponibles para la búsqueda.",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                },
            });

        },
        loadCustomerData: function () {
            var that = this;
            var controls = that.getControls();
            $("#lblTitle").text("TAREAS PROGRAMADAS");

            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
            Session.ACCESO = SessionTransac.SessionParams.USERACCESS;
            USERACCESS = SessionTransac.SessionParams.USERACCESS;

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

        },
        linkEdit_Click: function () {
            var that = this,
                controls = that.getControls();
            controls.tblScheduledTask.find('tbody a').addEvent(that, 'click', that.fnEditScheduledTasks);
        },
        linkDelete_Click: function () {
            var that = this,
                controls = that.getControls();

            controls.tblScheduledTask.find('tbody .deleteTasks').addEvent(that, 'click', that.fnDeleteScheduledTasks);
        },
        fnEditScheduledTasks: function (send, args) {
            var that = this,
                controls = that.getControls();
            var objRow = that.tblScheduledTasks.row($(send).parents('tr')).data();

            var _taskList = { SUSPENSION: 3, REACTIVACION: 4 }
            if ($.inArray(objRow.DescServi.toString(), Object.keys(_taskList)) != -1) {

                var objScheduledTransaction = {
                    Mode: 'Mod',
                    TServ: _taskList[objRow.DescServi],
                    SERVC_NUMBERACCOUNT: objRow.ServcNumberAccount,
                    CO_ID: objRow.CoId,
                    SERVC_STATE: objRow.ServcState,
                    SERVI_COD: objRow.ServiCode,
                   // COD_INTER: $(send).get(0).id
                };
                var objEditScheduledTasks = {
                    strIdSession: Session.IDSESSION,
                    objScheduledTransaction: objScheduledTransaction,

                };

                sessionStorage.setItem("Wsuspension", JSON.stringify(objEditScheduledTasks));

                $.window.open({

                    url: "/IFITransactions/SuspensionReconnectionCustomer/EditSuspensionReconnectionCustomer",
                    type: 'POST',

                    modal: false,
                    width: 990,
                    height: 590,
                    buttons: {

                        Cerrar: {
                            click: function (sender, args) {

                                that.btnSearch_click();
                                this.close();
                            }
                        }
                    }
                });



            }
            args.event.preventDefault();
        },
        fnDeleteScheduledTasks: function (send, args) {
            var that = this,
                controls = that.getControls();


            var objRow = that.tblScheduledTasks.row($(send).parents('tr')).data();


            var objScheduledTransactionModel = {

                CO_ID: objRow.CoId,
                SERVI_COD: objRow.ServiCode,
                SERVC_STATE: objRow.ServcState,
                SERVC_CODE_INTERACTION: objRow.ServcCodeInteraction
            };


            var objDeleteScheduledTasks = {
                strIdSession: Session.IDSESSION,
                objModel: objModel,
                StrStartDate: "",
                StrEndDate: ""

            };
            var objModel = {
                strIdSession: Session.IDSESSION,
                msisdn: Session.DATACUSTOMER.Telephone,
                fullNameUser: USERACCESS.fullName,
                contractId: DATACUSTOMER.ContractID,
                currentUser: USERACCESS.login,
                cboCACDAC: that.cacdac,
                customerId: DATACUSTOMER.CustomerID,
                tipo: TYPIFICATION.Tipo,
                claseDes: TYPIFICATION.ClaseDes,
                subClaseDes: TYPIFICATION.SubClaseDes,
                claseCode: TYPIFICATION.ClaseId,
                subClaseCode: TYPIFICATION.SubClaseId,
                tipoCode: TYPIFICATION.TipoId,

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
                strIdSession: Session.IDSESSION,
                cicloFacturacion: Session.CLIENTE.BillingCycle,
                objScheduledTransactionModel: objScheduledTransactionModel,
                email: Session.DATACUSTOMER.Email
            };

            confirm("¿Está seguro que desea borrar la programación?", "Confirmación",
                   function (result) {
                       if (!result) {
                           return false;
                       } else {
                           $.app.ajax({
                               type: 'POST',
                               contentType: "application/json; charset=utf-8",
                               dataType: 'json',
                               data: JSON.stringify(objModel),
                               url: '/IFITransactions/ScheduledTasks/DeleteScheduledTasks',
                               complete: function () {

                                   $.unblockUI();
                                   that.btnSearch_click();
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


                                   if (response.data != null) {
                                       alert(response.data);
                                       that.IsDelete = true;
                                   } else {
                                       alert("Hubo un problema al tratar de eliminar la Transaccion, vuelva mas tarde");
                                   }

                               }
                           });
                       }
                   });

            args.event.preventDefault();

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

                    that.cacdac = results.Cac;

                }
            });
        },
        btnClose_Click: function () {
            window.close();
        },
        setInitValues: function () {
            var that = this,
            controls = this.getControls();

            controls.txtContract.val(Session.DATACUSTOMER.ContratoID);
            controls.txtDateStart.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateEnd.datepicker({ format: 'dd/mm/yyyy' });
        },
        getStateType: function () {
            var that = this,
                controls = that.getControls(),
                objStateType = {};

            objStateType.strIdSession = Session.IDSESSION;
            objStateType.strIdList = "SERVC_ESTADO_HFC";

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objStateType),
                url: '/IFITransactions/ScheduledTasks/GetStateType',
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
                    controls.ddlStatus.append($('<option>', { value: '', html: 'Todos' }));

                    if (response.data != null) {
                        $.each(response.data.StateTypes, function (index, value) {
                            controls.ddlStatus.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        getTransactionType: function () {
            var that = this,
                controls = that.getControls(),
                objTransactionType = {};

            objTransactionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objTransactionType),
                url: '/IFITransactions/ScheduledTasks/GetTypeTransaction',
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
                    controls.ddlTypeTransaction.append($('<option>', { value: '', html: 'Todos' }));

                    if (response.data != null) {
                        $.each(response.data.TransactionTypes, function (index, value) {
                            controls.ddlTypeTransaction.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        getCacDacType: function () {
            var that = this,
                controls = that.getControls();
            objCacDacType = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: '/IFITransactions/CommonServices/GetCacDacType',
                complete: function () {
                    $.unblockUI();
                    that.getTipification();
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
                    controls.ddlCacDac.append($('<option>', { value: '', html: 'Todos' }));

                    if (response.data != null) {
                        $.each(response.data.CacDacTypes, function (index, value) {
                            controls.ddlCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        btnExportToExcel_click: function () {
            var that = this,
                controls = that.getControls(),
                strUrlResult = '/IFITransactions/CommonServices/DownloadExcel',

            oScheduledTasksRequest = {
                ServiCoId: controls.txtContract.val(),
                Account: controls.txtAccount.val(),
                StrStartDate: controls.txtDateStart.val(),
                StrEndDate: controls.txtDateEnd.val(),
                ServiceState: controls.ddlStatus.val(),
                Advisor: controls.txtAdvisor.val(),
                TransactionType: controls.ddlTypeTransaction.val(),
                CodeInteraction: controls.txtCodeTransaction.val(),
                NameCACDAC: controls.ddlCacDac.val(),
                strIdSession: Session.IDSESSION
            };

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '/IFITransactions/ScheduledTasks/ScheduledExportTransaction',
                data: JSON.stringify(oScheduledTasksRequest),
                success: function (path) {
                    strUrlResult = string.format('{0}/{1}', window.location.origin, strUrlResult.substring(1, strUrlResult.length));
                    window.location = strUrlResult + '?strPath=' + path + "&strNewfileName=ScheduledTasksLTE.xlsx";
                }
            });
        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
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

        $.fn.ScheduledTasksForm = function () {
            var option = arguments[0],
                args = arguments,
                value,
                allowedMethods = [];

            this.each(function () {

                var $this = $(this),
                    data = $this.data('ScheduledTasksForm'),
                    options = $.extend({}, $.fn.ScheduledTasksForm.defaults,
                        $this.data(), typeof option === 'object' && option);

                if (!data) {
                    data = new Form($this, options);
                    $this.data('ScheduledTasksForm', data);
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
    $.fn.ScheduledTasksForm.defaults = {
    }
    $('#ScheduledTasksForm').ScheduledTasksForm();
})(jQuery);