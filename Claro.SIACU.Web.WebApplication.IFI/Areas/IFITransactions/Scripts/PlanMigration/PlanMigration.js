var globalVar = {};

function f_cancelAuth() {
    $('#txtDate').datepicker('setDate', globalVar.strApplicationDate);
};

function CloseValidation(obj, pag, controls) {
    var mensaje;
    if (obj.hidAccion === 'G') {// Correcto

    } else { 
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");
        $('#txtDate').datepicker('setDate', globalVar.strApplicationDate);
        return;
    }
};

(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTPlanMigration.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblServiceNumber: $("#lblServiceNumber", $element)
            , lblCustomerID: $('#lblCustomerID', $element)
            , lblCustomerType: $("#lblCustomerType", $element)
            , lblCustomer: $("#lblCustomer", $element)
            , lblCustomerContact: $("#lblCustomerContact", $element)
            , lblDocument: $('#lblDocument', $element)
            , lblLegalRepres: $('#lblLegalRepres', $element)
            , lblCurrentPlan: $("#lblCurrentPlan", $element)
            , lblActivationDate: $("#lblActivationDate", $element)
            , lblBillingCycle: $('#lblBillingCycle', $element)
            , lblCredit: $('#lblCredit', $element)

            , lblAddress: $("#lblAddress", $element)
            , lblReference: $("#lblReference", $element)
            , lblCountry: $("#lblCountry", $element)
            , lblDepartment: $("#lblDepartment", $element)
            , lblProvince: $("#lblProvince", $element)
            , lblDistrict: $("#lblDistrict", $element)
            , lblPlano: $("#lblPlano", $element)
            , lblUbigeo: $("#lblUbigeo", $element)

            , spnCurrentPlan: $("#spnCurrentPlan", $element)
            , spnNewPlan: $("#spnNewPlan", $element)
            , btnAddPlan: $("#btnAddPlan", $element)

            , lstCurrentPlanInternet: $("#lstCurrentPlanInternet", $element)
            , lstSelectPlanInternet: $("#lstSelectPlanInternet", $element)
            , spnCurrentTotalFixedChargeCIGV: $("#spnCurrentTotalFixedChargeCIGV", $element)
            , spnNewTotalFixedChargeCIGV: $("#spnNewTotalFixedChargeCIGV", $element)

            , txtDate: $('#txtDate', $element)
            , txtCalendarDate: $('#txtCalendarDate', $element)
            , txtRefund: $('#txtRefund', $element)
            , txtPenalty: $('#txtPenalty', $element)
            , txtTotalPenalty: $('#txtTotalPenalty', $element)
            , chkLoyaltyPenalty: $('#chkLoyaltyPenalty', $element)
            , txtTotalDiscountPenalty: $('#txtTotalDiscountPenalty', $element)

            , txtNumberReference: $('#txtNumberReference', $element)
            , chkEmail: $("#chkEmail", $element)
            , txtEmail: $("#txtEmail", $element)
            , ddlPointOfAttention: $("#ddlPointOfAttention", $element)
            , txtNotes: $('#txtNotes', $element)

            , divErrorAlert: $('#divErrorAlert', $element)
            , lblErrorMessage: $("#lblErrorMessage", $element)
            , lblTitle: $('#lblTitle', $element)
            , divRules: $('#divRules', $element)
            
            , btnClose: $("#btnClose", $element)
            , btnSave: $("#btnSave", $element)
            , btnConstance: $("#btnConstance", $element)
            , btnAddServ: $('#btnAddServ', $element)
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();
            controls.btnClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnConstance.addEvent(that, 'click', that.btnConstance_click);
            controls.txtDate.datepicker({ format: 'dd/mm/yyyy', startDate: new Date() });
            controls.txtDate.addEvent(that, 'change', that.changeDate);
            controls.txtCalendarDate.addEvent(that, 'click', that.txtCalendarDate_click);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_Change);
            controls.btnAddPlan.addEvent(that, 'click', that.btnAddPlan_Pop);
            controls.btnAddServ.addEvent(that, 'click', that.btnAddServ_Pop);

            global_lstCurrentServices = [];
            global_oTypification = {};
            global_IGV = 0;
            global_Office = '';

            controls.txtRefund.val('0');
            controls.txtPenalty.val('0');
            controls.txtTotalPenalty.val('0');
            controls.txtTotalDiscountPenalty.val('0');
            controls.chkEmail.prop('checked', true);

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        render: function () {

            var that = this,
            controls = that.getControls();
            controls.divErrorAlert.hide();
            controls.btnConstance.prop('disabled', true);
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.URLPARAMS = SessionTransac.UrlParams;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            that.loadData();
        },
        loadData: function () {
            var that = this,
                controls = this.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE,
                oUserAccess = Session.USERACCESS;

            controls.lblTitle.text("CAMBIO DE PLAN");

            //********** Datos del Cliente ***********/
            controls.lblServiceNumber.html((oCustomer.Telephone == null) ? '' : oCustomer.Telephone);
            controls.lblCustomerID.html((oCustomer.CustomerID == null) ? '' : oCustomer.CustomerID);
            controls.lblCustomerType.html((oCustomer.CustomerType == null) ? '' : oCustomer.CustomerType);
            controls.lblCustomer.html((oCustomer.BusinessName == null) ? '' : oCustomer.BusinessName);
            controls.lblCustomerContact.html((oCustomer.FullName == null) ? '' : oCustomer.FullName);
            controls.lblDocument.html((oCustomer.DNIRUC == null) ? '' : oCustomer.DNIRUC);
            controls.lblLegalRepres.html((oCustomer.LegalAgent == null) ? '' : oCustomer.LegalAgent);
            controls.lblCurrentPlan.html((oDataService.Plan == null) ? '' : oDataService.Plan);
            controls.lblActivationDate.html((oCustomer.ActivationDate == null) ? '' : oCustomer.ActivationDate);
            controls.lblBillingCycle.html((oCustomer.objPostDataAccount.BillingCycle == null) ? '' : oCustomer.objPostDataAccount.BillingCycle);
            controls.lblCredit.html((oCustomer.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + oCustomer.objPostDataAccount.CreditLimit);
            //********** Direccíón de Facturación ***********/
            controls.lblAddress.html((oCustomer.InvoiceAddress == null) ? '' : oCustomer.InvoiceAddress);
            controls.lblReference.html((oCustomer.InvoiceUrbanization == null) ? '' : oCustomer.InvoiceUrbanization);
            controls.lblCountry.html((oCustomer.InvoiceCountry == null) ? '' : oCustomer.InvoiceCountry);
            controls.lblDepartment.html((oCustomer.InvoiceDepartament == null) ? '' : oCustomer.InvoiceDepartament);
            controls.lblProvince.html((oCustomer.InvoiceProvince == null) ? '' : oCustomer.InvoiceProvince);
            controls.lblDistrict.html((oCustomer.InvoiceDistrict == null) ? '' : oCustomer.InvoiceDistrict);
            controls.lblPlano.html((oCustomer.PlaneCodeBilling == null) ? '' : oCustomer.PlaneCodeBilling);
            controls.lblUbigeo.html((oCustomer.InvoiceUbigeo == null) ? '' : oCustomer.InvoiceUbigeo);

            controls.spnCurrentPlan.html((oDataService.Plan == null) ? '' : oDataService.Plan);
            controls.txtEmail.val((oCustomer.Email == null) ? '' : oCustomer.Email);

            that.validateTransacction();
        },
        validateTransacction: function(){
            var that = this,
                controls = this.getControls(),
                objValidate = {};

            objValidate.strIDSession = Session.IDSESSION;
            objValidate.strTransaction = "";
            objValidate.intValidationType = "5";
            objValidate.strStatusLine = Session.DATASERVICE.StateLine;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objValidate),
                url: '/IFITransactions/CommonServices/GetStatusLineMsgValidate',
                success: function (response) {
                    if (response.data != '') {
                        controls.btnAddPlan.prop("disabled", true);
                        controls.btnSave.prop("disabled", true);
                        alert(response.data, 'Informativo', function () {
                            parent.window.close();
                        });
                    } else {
                        that.LoadTransaction();
                    }
                }
            });
        },
        LoadTransaction: function(){
            var that = this,
                controls = this.getControls(),
                objLoadParameters = {};

            that.Loading();

            objLoadParameters.strIDSession = Session.IDSESSION;
            objLoadParameters.strUser = Session.USERACCESS.login;
            objLoadParameters.strContract = Session.DATACUSTOMER.ContractID;
            objLoadParameters.strBillingCycle = Session.DATACUSTOMER.objPostDataAccount.BillingCycle;
            objLoadParameters.strPermitions = Session.USERACCESS.optionPermissions;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLoadParameters),
                url: '/IFITransactions/PlanMigration/PlanMigrationLoad',
                success: function (response) {
                    var strSummary = '';

                    if (response.data.oOffce != null) {
                        global_Office = response.data.oOffce.strCodeOffice;
                        if (global_Office == '') {
                            strSummary = strSummary + 'El usuario logueado no tienen asociado ningún punto de atención. <br />';
                        }
                    }

                    if (response.data.lstPointOfAttention != null && response.data.lstPointOfAttention.length > 0) {
                        that.loadPointOfAttention(response.data.lstPointOfAttention, response.data.strUserxCac);
                    } else {
                        strSummary = strSummary + 'No se cargaron los puntos de atención.<br />';
                    }

                    if (response.data.lstBusinessRules != null) {
                        that.loadBusinessRules(response.data.lstBusinessRules);
                    } else {
                        strSummary = strSummary + 'No se cargaron las reglas de negocio.<br />';
                    }

                    if (response.data.dblIGVPercent == null) {
                        strSummary = strSummary + 'No se cargó el porcentaje de IGV.<br />';
                    } else {
                        global_IGV = response.data.dblIGVPercent;
                    }

                    if (response.data.lstCurrentServices == null) {
                        strSummary = strSummary + 'No se cargaron los servicios actuales del cliente.<br />';
                    } else {
                        global_lstCurrentServices = response.data.lstCurrentServices;
                    }


                    if (response.data.oTypification == null) {
                        strSummary = strSummary + 'No se cargaron los códigos de tipificación.<br />';
                    } else {
                        global_oTypification = response.data.oTypification;
                    }

                    //Session.strProductType = ((Session.URLPARAMS.SUREDIRECT == 'IFI') ? '06' : '08');
                    Session.strProductType = '06';
                    Session.dblIGVPercentUnity = response.data.dblIGVPercertUnity;
                    Session.strCurrentGroupInternet = response.data.strCurrentGroupInternet;
                    Session.strCurrentTMCode = response.data.strCurrentTMCode;
                    Session.strEditApplicationDate = response.data.strEditApplicationDate;
                    globalVar.strEditApplicationDate = response.data.strEditApplicationDate;
                    that.strPDFServer = response.data.strPDFServer;
                    
                    if (response.data.strSIMCard == null) {
                        strSummary = strSummary + 'No se cargó el SIM del equipo del cliente.<br />';
                    } else {
                    Session.strSIMCard = response.data.strSIMCard;
                    }

                    globalVar.strApplicationDate = response.data.strApplicationDate;
                    controls.txtDate.datepicker('setDate', response.data.strApplicationDate);

                    that.getCurrentPlanServices(global_lstCurrentServices);

                    if (strSummary != '') {
                        controls.divErrorAlert.show(); controls.lblErrorMessage.html(strSummary);
                    }
                }
            });
        },
        loadPointOfAttention: function (lstPointOfAttention, strUserxCac) {
            var that = this,
                controls = that.getControls();

            if (lstPointOfAttention.length > 0) {
                controls.ddlPointOfAttention.append($('<option>', { value: '', html: 'Seleccionar' }));
                var itemSelect;
                $.each(lstPointOfAttention, function (index, value) {
                    if (strUserxCac === value.Description) {
                        controls.ddlPointOfAttention.append($('<option>', { value: value.Code, html: value.Description }));
                        itemSelect = value.Code;

                    } else {
                        controls.ddlPointOfAttention.append($('<option>', { value: value.Code, html: value.Description }));
                    }
                });
                if (itemSelect != null && itemSelect.toString != "undefined") {
                    $("#ddlPointOfAttention option[value=" + itemSelect + "]").attr("selected", true);
                }

            }
        },
        loadBusinessRules: function (lstBusinessRules) {
            var that = this,
                controls = that.getControls();
            if (lstBusinessRules.length > 0) {
                controls.divRules.append(lstBusinessRules[0].Description);
            }

        },
        getCurrentPlanServices: function (lstServicesByCurrentPlan) {
            var that = this;
            var controls = that.getControls();

            var intInternet = 0;
            var dblTotalCF = 0;

            if (lstServicesByCurrentPlan != null) {
                $.each(lstServicesByCurrentPlan, function (index, item) {
                    item.FixedChargeWithIgv = item.FixedCharge * parseFloat(Session.dblIGVPercentUnity);
                    if (Session.strCurrentGroupInternet.indexOf(item.GroupNo) > -1) {
                        if (item.ServiceType.toUpperCase() === "CORE") {
                            controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.FixedChargeWithIgv.toFixed(2) + '</span> ' + item.ServiceDes + '</li>');
                        } else {
                            controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.FixedChargeWithIgv.toFixed(2) + '</span> <b>' + item.ServiceDes + '</b></li>');
                        }
                        intInternet++;
                        dblTotalCF = dblTotalCF + parseFloat(item.FixedChargeWithIgv);
                    }
                });
            }

            var fltNewTotalFixedChargeCIGV = parseFloat(controls.spnCurrentTotalFixedChargeCIGV.text().substring(3));
            fltNewTotalFixedChargeCIGV = fltNewTotalFixedChargeCIGV + dblTotalCF;
            controls.spnCurrentTotalFixedChargeCIGV.text('S/.' + fltNewTotalFixedChargeCIGV.toFixed(2));

            if (intInternet == 0) controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item transac-message-red text-center"> ' + 'No existen Registros.' + ' </li>');

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
        btnClose_Click: function () {
            parent.window.close();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        chkEmail_Change: function (sender, arg) {
            var that = this,
                controls = that.getControls(),
                chkEmail = controls.chkEmail;

            if (chkEmail[0].checked == true) {
                controls.txtEmail.css("display", "block");
            } else {
                controls.txtEmail.css("display", "none");
            }
        },
        changeDate: function () {

            var that = this,
                controls = this.getControls();

            if (Session.strEditApplicationDate == "0") {
                if (controls.txtDate.val() != globalVar.strApplicationDate) {
                    var co = '500';
                    var param = {
                        "strIdSession": Session.IDSESSION,
                        'pag': '1',
                        'opcion': Session.strEditApplicationDate,
                        'co': co
                    };
                    ValidateAccess(that, controls, 'IMP', 'gConstEvtFechaAplicacionCP', '1', param, 'Fixed');
                }
            }
        },
        txtCalendarDate_click: function () {
            var that = this,
                controls = that.getControls();

        },
        btnAddPlan_Pop: function () {
            var that = this,
                controls = that.getControls();

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            urlBase = location.protocol + '//' + location.host + '/IFITransactions/PlanMigration/ChoosePlan';


            var dialog = $.window.open({
                modal: true,
                type: 'post',
                title: "Seleccionar Nuevo Plan",
                url: urlBase,
                width: 1024,
                height: 620,
                buttons: {
                    Seleccionar: {
                        click: function () {
                            var rowPost = $('#tblPlans').DataTable().rows({ selected: true }).data();
                            var item = rowPost[0];
                            if (item === undefined) {
                                alert("Debe seleccionar un plan.", "Alerta");
                                return false;
                            }
                            that.charguePlan_Pop(item.strSisactPlanCode, item.strSisactPlanDes, item.strCampaignCode, this); ///////INC000003633281
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            Session.NewPlanName = '';
                            Session.NewPlanId = '';
                            this.close();
                        }
                    }
                }
            });

        },
        charguePlan_Pop: function (idNewPlan, strNewPlan, strCampaignCode, objPopUp) {///////INC000003633281
            var that = this,
                controls = that.getControls(),
                strAlertMsg = 'Si cambia el plan, se borrarán los datos cargados en las tablas... ¿Desea cargar un nuevo plan?';

            confirm(strAlertMsg, "Confirmar", function () {
                $.blockUI({
                    message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
                Session.NewPlanName = strNewPlan;
                Session.NewCampaignCode = strCampaignCode;///////INC000003633281
                Session.NewPlanId = idNewPlan;

                objPopUp.close();
                that.btnChooseCoreServicesByPlan_Pop();
                $.unblockUI();
            });
        },
        btnChooseCoreServicesByPlan_Pop: function () {
            var that = this,
                controls = that.getControls();
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            urlBase = location.protocol + '//' + location.host + '/IFITransactions/PlanMigration/ChooseServicesByPlan';
            Session.idPlan = Session.NewPlanId;

            var dialog = $.window.open({
                modal: true,
                title: "Seleccionar Servicios Core",
                url: urlBase,
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {
                        id: "btnSelectCore",
                        click: function (e) {
                            var ModalConfirm = this;

                            var objRowPostInternetTotal = $('#tblChooseCoreServicesByPlanInternet').DataTable().rows().data();
                            var objRowPostInternet = $('#tblChooseCoreServicesByPlanInternet').DataTable().rows({ selected: true }).data();

                            if (objRowPostInternetTotal.length == 0 && objRowPostInternet.length == 0) {
                                alert('El plan seleccionado no tiene servicios asociados, seleccione otro plan por favor.', 'Informativo', function () { });
                                return false;
                            }

                            if (objRowPostInternetTotal.length > 0 && objRowPostInternet.length == 0) {
                                alert('Debe seleccionar al menos un servicio core de Internet', 'Informativo', function () { });
                                return false;
                            }

                            confirm('¿Está seguro que desea cargar los Core seleccionados?', 'Confirmar', function (result) {
                                if (result) {
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

                                    Session.idPlan = Session.NewPlanId;
                                    controls.spnNewPlan.text(Session.NewPlanName);
                                    that.CleaningServiceList();
                                    that.CleaningObjectSave();
                                    that.ShowServiceCoreSelected(null, objRowPostInternet[0], null);

                                    $.unblockUI();

                                    ModalConfirm.close();
                                }
                            });

                        }
        },
                    Cancelar: {
                        click: function (sender, args) {
                            Session.idPlan = Session.idNewPlan;
                            Session.NewPlanName = '';
                            Session.NewPlanId = '';
                            Session.NewCampaignCode = '';///////INC000003633281
                            this.close();
                        }
                    }
                }
            });

        },
        btnAddServ_Pop: function () {
            var that = this,
               controls = that.getControls();

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var that = this;

            var dialog = $.window.open({
                modal: true,
                title: "Seleccionar Servicios Adicionales",
                url: '/IFITransactions/PlanMigration/ChooseServicesByPlan',
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {
                        click: function () {
                            var ModalConfirm = this;
                            var rowPostCable = $('#tblChooseServicesByPlanCable').DataTable().rows({ selected: true }).data();
                            var rowPostInternet = $('#tblChooseServicesByPlanInternet').DataTable().rows({ selected: true }).data();
                            var rowPostPhone = $('#tblChooseServicesByPlanPhone').DataTable().rows({ selected: true }).data();
                            if (rowPostCable.length === 0 && rowPostInternet === 0 && rowPostPhone === 0) {
                                alert("Necesita seleccionar un servicio.", "Alerta");
                                return false;
                            }

                            confirm('Los adicionales seleccionados se cargarán, ¿desea continuar?', 'Confirmar', function (result) {
                                if (result) {
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

                                    $.unblockUI();

                                    ModalConfirm.close();
                                }
                            });

                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        ShowServiceCoreSelected: function (objRowPostCable, objRowPostInternet, objRowPostPhone) {
            var that = this,
                controls = that.getControls(),
                objRequestData = {};
            objRequestData.strIdSession = Session.IDSESSION;
            objRequestData.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestData.strProductType = Session.strProductType;
            objRequestData.strIdPlan = Session.idPlan;
            objRequestData.strIgv = Session.dblIGVPercentUnity;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestData),
                url: location.protocol + '//' + location.host + '/IFITransactions/PlanMigration/GetAdditionalCoreServicesNEquipment',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        var obj = response.data;

                        if (obj.lstAdditionalCoreServicesInternet == null)
                            obj.lstAdditionalCoreServicesInternet = [];
                        that.objPlanMigrationSave.lstAdditionalCoreServicesInternet = obj.lstAdditionalCoreServicesInternet;

                        that.LoadAdditionalCoreServices(objRowPostInternet, controls.lstSelectPlanInternet, obj.lstAdditionalCoreServicesInternet);
                    }
                }
            });
        },
        LoadAdditionalCoreServices: function (lstServicesCore, control, lstAdditionalServicesCore) {
            var that = this;
            var controls = that.getControls();
            var intCount = 0;

            if (lstServicesCore != null && lstServicesCore !== undefined) {
                control.append('<li class="transac-list-group-item"><span class="badge">S/ ' + that.IsNull(lstServicesCore.CfWithIgv.toFixed(2), "0") + '</span> ' + that.IsNull(lstServicesCore.SisactServDes, "") + '</li>');
                that.addAmountNewPlan("CORE", lstServicesCore);
                that.objPlanMigrationSave.lstCoreServices.push(lstServicesCore);
                intCount++;
            }
            $.each(lstAdditionalServicesCore, function (index, item) {
                control.append('<li class="transac-list-group-item"><span class="badge">S/ ' + that.IsNull(item.CfWithIgv.toFixed(2), "0") + '</span> ' + that.IsNull(item.SisactServDes, "") + '</li>');
                that.addAmountNewPlan("CORE-ADICIONAL", item);
                intCount++;
                that.objPlanMigrationSave.lstCoreServices.push(item);
            });
            if (intCount == 0) {
                control.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');

            }
        },
        addAmountNewPlan: function (strType, objService) {
            var that = this,
              controls = that.getControls();

            if (objService != null && objService !== undefined) {
                var fltAmount = objService.CfWithIgv;

                var fltNewTotalFixedChargeCIGV = parseFloat(controls.spnNewTotalFixedChargeCIGV.text().substring(3));
                fltNewTotalFixedChargeCIGV = fltNewTotalFixedChargeCIGV + fltAmount;
                controls.spnNewTotalFixedChargeCIGV.text('S/.' + fltNewTotalFixedChargeCIGV.toFixed(2));

            }
        },
        IsNull: function (object, defaultValue) {
            if (object == null)
                return defaultValue;
            return object;
        },
        CleaningServiceList: function () {
            var that = this;
            var controls = that.getControls();
            controls.lstSelectPlanInternet.html('');
            controls.spnNewTotalFixedChargeCIGV.text("S/.0");
        },
        CleaningObjectSave: function () {
            var that = this;
            var controls = that.getControls();
            that.objPlanMigrationSave.lstServices = [];
            that.objPlanMigrationSave.lstCoreServices = [];
        },
        btnSave_click: function () {
            var that = this,
               controls = that.getControls(),
               objProgramTask = {},
               objInteraction = {},
               objInteractionTemplate = {},
               objPlanMigrationSave = {};

            if (Session.NewPlanId == '' || Session.NewPlanId == null) {
                modalAlert("Seleccione un nuevo plan.", "Error");
                return false;
            }

            if (controls.ddlPointOfAttention.val() == '') {
                modalAlert('Seleccione un punto de atención.', 'Error');
                return false;
            }

            if (controls.chkEmail.prop('checked')) {
                var validaEmail = $.IsEmail(controls.txtEmail.val());
                if (!validaEmail) {
                    modalAlert('Ingrese un correo electrónico correcto.', 'Error');
                    return false;
                }
        }

            if (controls.txtNotes.val().trim() == '') {
                modalAlert('El campo notas no puede estar vacio.', 'Error');
                return false;
            }

            var strNotes = controls.txtNotes.val();
            var strFixedNotes = strNotes.replace(/[\n\r\t]/g, " ");

            objProgramTask.SERVV_MSISDN = Session.DATACUSTOMER.Telephone; //servvMsisd
            objProgramTask.SERVD_DATEPROG = controls.txtDate.val(); //servdFechaProg
            objProgramTask.CO_ID = Session.DATACUSTOMER.ContractID; //codId
            objProgramTask.CUSTOMER_ID = Session.DATACUSTOMER.CustomerID; //customerId
            objProgramTask.SERVC_CO_SER = ''; //serviCoser
            objProgramTask.SERVV_USER_SYSTEM = Session.USERACCESS.login; //servvUsuarioSis
            objProgramTask.SERVV_USER_APLICATION = Session.USERACCESS.login; //servvUsuarioApp
            objProgramTask.SERVV_EMAIL_USER_APP = ''; //servvEmailUsuarioApp
            objProgramTask.SERVC_DES_CO_SER = ''; //descCoSer
            objProgramTask.SERVC_NUMBERACCOUNT = Session.DATACUSTOMER.Account; //servcNrocuenta

            objInteraction.ObjidContacto = ''; //contactoObjId
            objInteraction.InsertPor = '1'; //flagReg
            objInteraction.Cuenta = Session.DATACUSTOMER.Account; //account
            objInteraction.Telephone = Session.DATACUSTOMER.Telephone; //phone
            objInteraction.Type = global_oTypification.Type; //tipo
            objInteraction.Class = global_oTypification.Class; //clase
            objInteraction.SubClass = global_oTypification.SubClass; //subclase
            objInteraction.Agenth = Session.USERACCESS.login; //agente
            objInteraction.Note = strFixedNotes; //notas
            objInteraction.Plan = Session.DATACUSTOMER.PlaneCodeBilling; //codPlano
            
            objInteractionTemplate.X_CLAROLOCAL1 = Session.DATACUSTOMER.BusinessName; //Customer
            objInteractionTemplate.X_CLAROLOCAL2 = Session.DATACUSTOMER.FullName; //Contact
            objInteractionTemplate.X_DOCUMENT_NUMBER = Session.DATACUSTOMER.DocumentNumber; //Document Number
            objInteractionTemplate.X_INTER_15 = Session.DATACUSTOMER.ActivationDate; //Activation Date
            objInteractionTemplate.X_AMOUNT_UNIT = Session.DATACUSTOMER.objPostDataAccount.BillingCycle; //Billing Cycle
            objInteractionTemplate.X_REASON = Session.DATASERVICE.TermContract; // Type of agreement
            objInteractionTemplate.X_POSITION = Session.DATASERVICE.StateLine; // State Line
            objInteractionTemplate.X_INTER_16 = Session.DATACUSTOMER.objPostDataAccount.ExpirationDate; //End Date

            objInteractionTemplate.X_INTER_7 = '0'; //ID Call
            //objInteractionTemplate.X_BIRTHDAY - Current Date
            objInteractionTemplate.X_INTER_1 = controls.spnCurrentPlan.text(); //Current Plan
            objInteractionTemplate.X_INTER_2 = controls.spnNewPlan.text(); //New Plan
            objInteractionTemplate.X_INTER_3 = controls.spnCurrentTotalFixedChargeCIGV.text(); //Current Fixed Charged
            objInteractionTemplate.X_INTER_4 = controls.spnNewTotalFixedChargeCIGV.text(); //New Fixed Charged
            objInteractionTemplate.X_INTER_17 = controls.txtRefund.val(); //Refund
            objInteractionTemplate.X_INTER_5 = controls.txtDate.val(); //Application Date
            objInteractionTemplate.X_INTER_18 = controls.txtPenalty.val(); //Penalty
            objInteractionTemplate.X_INTER_19 = controls.txtTotalPenalty.val(); //Total Penalty

            if (controls.chkLoyaltyPenalty.prop('checked')) {
                objInteractionTemplate.X_FLAG_OTHER = '1'; //Loyalty Flag
            } else {
                objInteractionTemplate.X_FLAG_OTHER = '0'; //Loyalty Flag
            }
            
            objInteractionTemplate.X_INTER_6 = $('#ddlPointOfAttention option:selected').text() //Attention Point
            objInteractionTemplate.X_REFERENCE_PHONE = controls.txtNumberReference.val(); //Reference number

            if (controls.chkEmail.prop('checked')) {
                objInteractionTemplate.X_EMAIL_CONFIRMATION = '1';
                objInteractionTemplate.X_EMAIL = controls.txtEmail.val();
            } else {
                objInteractionTemplate.X_EMAIL_CONFIRMATION = '0';
                objInteractionTemplate.X_EMAIL = '';
            }
            objInteractionTemplate.X_INTER_30 = strFixedNotes; //Notes
            objInteractionTemplate.X_NAME_LEGAL_REP = Session.DATACUSTOMER.LegalAgent; //Legal Rep
            objInteractionTemplate.X_CLARO_NUMBER = Session.DATACUSTOMER.Telephone; //Telephone
            objInteractionTemplate.X_TYPE_DOCUMENT = Session.DATACUSTOMER.DocumentType; //Document Type
            objInteractionTemplate.X_CLAROLOCAL5 = Session.USERACCESS.login; //User
            objInteractionTemplate.X_ICCID = Session.USERACCESS.fullName; //Username
            objInteractionTemplate._X_FLAG_CONTINGENCY = '0';

            objPlanMigrationSave.oProgramTask = objProgramTask;
            objPlanMigrationSave.oInteraction = objInteraction;
            objPlanMigrationSave.oInteractionTemplate = objInteractionTemplate;
            objPlanMigrationSave.strPenalty = '0';
            objPlanMigrationSave.strTotalPenalty = '0';
            objPlanMigrationSave.strRefund = '0';
            objPlanMigrationSave.strFlagPenalty = '0';
            objPlanMigrationSave.strFlagLoyaltyPenalty = '0';
            objPlanMigrationSave.strTotalDiscountPenalty = '0';
            objPlanMigrationSave.strOCCPenalty = '123';
            objPlanMigrationSave.strOCCPenaltyConcept = '123';
            objPlanMigrationSave.strIdeaiWS = '';
            objPlanMigrationSave.strPhone = Session.DATACUSTOMER.CustomerID;
            objPlanMigrationSave.lstCurrentServices = global_lstCurrentServices;
            objPlanMigrationSave.lstNewServices = that.objPlanMigrationSave.lstCoreServices;
            objPlanMigrationSave.strIDSession = Session.IDSESSION;
            objPlanMigrationSave.strCurrentTMCode = Session.strCurrentTMCode;
            objPlanMigrationSave.strSIMCard = Session.strSIMCard;
            objPlanMigrationSave.strCodProduct = Session.idPlan;
            objPlanMigrationSave.strCampaignCode = Session.NewCampaignCode;////INC000003633281

            confirmAlert('¿Está seguro que desea programar el Cambio de Plan?', 'Confirmar', function (res) {
                if (res) {

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


                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(objPlanMigrationSave),
                        url: location.protocol + '//' + location.host + '/IFITransactions/PlanMigration/PlanMigrationSave',
                        error: function (xhr, status, error) {
                            $.unblockUI();
                        },
                        success: function (response) {
                            var msj = '';
                            var msjdiv = '';
                            if (response.data == null) {
                                msj = "Error en la conectividad con el WS.";
                                msjdiv = msj;
                                modalAlert(msj, "Informativo");
                            } else {
                                if (response.data.MessageResponse.Body.Fault != null) {
                                    msj = "Error en la conectividad con el WS.";
                                    msjdiv = "Error: " + response.data.MessageResponse.Body.Fault.Detail.IntegrationError;
                                    modalAlert(msj, "Informativo");
                                } else {
                                    if (response.data.MessageResponse.Body.codigoRespuesta == '0') {
                                            msj = "Programación generada correctamente." + "<br />" + "El código de interacción es: " + response.data.MessageResponse.Body.idInteraccion;
                                            modalAlert(msj, "informativo");
                                            globalVar.strPDFRoute = response.data.MessageResponse.Body.rutaConstancia;
                                            controls.btnConstance.prop('disabled', false);
                                    } else {
                                        msj = "Error: " + response.data.MessageResponse.Body.mensajeRespuesta;
                                        modalAlert(msj, "Error");
                                    }
                                }
                            }

                            if (msjdiv != '') {
                            controls.divErrorAlert.show(); controls.lblErrorMessage.html(msjdiv);
                            }

                            $.unblockUI();
                            controls.btnSave.prop("disabled", true);
                        }
                    });

                }
            });

        },
        btnConstance_click: function () {
            var that = this,
              controls = that.getControls();

            if (globalVar.strPDFRoute != '') {
                var newRoute = globalVar.strPDFRoute.substring(globalVar.strPDFRoute.indexOf('SIACUNICO'));
                newRoute = newRoute.replace(new RegExp('/', 'g'), '\\');
                newRoute = that.strPDFServer + newRoute;

                ReadRecordSharedFile(Session.IDSESSION, newRoute);
            } else {
                alert('No se ha cargado correctamente el archivo de la constancia.', "Alerta");
            }
        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        objPlanMigrationSave: [],
        strPDFServer: ''
    };

    $.fn.INTPlanMigration = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTPlanMigration'),
                options = $.extend({}, $.fn.INTPlanMigration.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTPlanMigration', data);
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

    $.fn.INTPlanMigration.defaults = {
    }

    $('#divBody').INTPlanMigration();
})(jQuery);