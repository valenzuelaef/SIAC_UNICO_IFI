(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTChangeBillingCycle.defaults, $element.data(), typeof options === 'object' && options);
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

            , ddlNuevoCicloFact: $('#ddlNuevoCicloFact', $element)
            , ddlCACDAC: $("#ddlCACDAC", $element)
            , chkEmail: $("#chkEmail", $element)

            , txtEmail: $("#txtEmail", $element)
            , txtNote: $('#txtNote', $element)
            , txtDate: $('#txtDate', $element)
            , txtCalendarDate: $('#txtCalendarDate', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnConstancia: $("#btnConstancia", $element)
            , cboNewCycle: $("#cboNewCycle", $element)
            , txtDateCycle: $("#txtDateCycle", $element)
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
            // controls.txtDate.datepicker({ format: 'dd/mm/yyyy' });
            // controls.txtDate.addEvent(that, 'change', that.changeDate);
            // controls.txtCalendarDate.addEvent(that, 'click', that.txtCalendarDate_click);
            controls.txtDateCycle.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateCycle.addEvent(that, 'change', that.changeDateCommitment);
            controls.cboNewCycle.addEvent(that, 'change', that.cboNewCycle_change);
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        cboNewCycle_change: function () {
            var that = this,
           controls = that.getControls(),
           objCyclesBilling = {};

            objCyclesBilling.strIdSession = Session.IDSESSION;
            objCyclesBilling.value = $('#cboNewCycle option:selected').text();


            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCyclesBilling),
                url: '/IFITransactions/ChangeBillingCycle/GetDateAplication',
                success: function (response) {
                    
                    controls.txtDateCycle.val(response.data);
                }
            });
        },
        modelChangeBillingCycle: {},
        render: function () {

            var that = this,
            control = that.getControls();
            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            that.Loading();
            that.loadData();
            that.LoadCacs();
            that.LoadCyclesBilling();
            $("#chkEmail").prop("checked", true)

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
        LoadCyclesBilling: function () {
           


            var that = this,
                controls = that.getControls(),
                objCyclesBilling = {};

            objCyclesBilling.strIdSession = Session.IDSESSION;
            objCyclesBilling.strCustomerID = Session.DATACUSTOMER.CodCustomerType;


            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCyclesBilling),
                url: '/IFITransactions/ChangeBillingCycle/GetListCyclesBilling',
                success: function (response) {
                   
                    controls.cboNewCycle.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboNewCycle.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });

        }
        ,
        LoadCacs: function () {
           


            var that = this,
                controls = that.getControls(),
                objCacDacType = {},
                parameters = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.login;

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
                            if (response.data != null) {
                                var itemSelect;
                               
                                $.each(response.data.CacDacTypes, function (index, value) {
                                    
                                    if (cacdac == value.Description) {
                                       
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
                        }
                    });
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

        btnCerrar_Click: function () {
            parent.window.close();
        },


        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },

        loadData: function () {
            var that = this,
                controls = this.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE,
                oUserAccess = Session.USERACCESS;

            controls.lblTitle.text("CAMBIO DE CICLO DE FACTURACIÓN IFI");

            //********** Datos del Cliente ***********/
            controls.lblContrato.html((oCustomer.ContractID == null) ? '' : oCustomer.ContractID);
            controls.lblCustomerID.html((oCustomer.CustomerID == null) ? '' : oCustomer.CustomerID);
            controls.lblTipoCliente.html((oCustomer.CustomerType == null) ? '' : oCustomer.CustomerType);
            controls.lblCliente.html((oCustomer.BusinessName == null) ? '' : oCustomer.BusinessName);
            controls.lblContacto.html((oCustomer.FullName == null) ? '' : oCustomer.FullName);
            controls.lblDNI_RUC.html((oCustomer.DNIRUC == null) ? '' : oCustomer.DNIRUC);
            controls.lblRepren_Legal.html((oCustomer.LegalAgent == null) ? '' : oCustomer.LegalAgent);
            controls.lblPlan.html((oDataService.Plan == null) ? '' : oDataService.Plan);
            controls.lblFechaActivacion.html((oCustomer.ActivationDate == null) ? '' : oCustomer.ActivationDate);
            controls.lblCicloFact.html((oCustomer.objPostDataAccount.BillingCycle == null) ? '' : oCustomer.objPostDataAccount.BillingCycle);
            controls.lblLimiteCred.html((oCustomer.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + oCustomer.objPostDataAccount.CreditLimit);
            //********** Direccíón de Facturación ***********/
            controls.lblDireccion.html((oCustomer.InvoiceAddress == null) ? '' : oCustomer.InvoiceAddress);
            controls.lblNotasDirec.html((oCustomer.LegalUrbanization == null) ? '' : oCustomer.LegalUrbanization);
            controls.lblPais.html((oCustomer.Country == null) ? '' : oCustomer.Country);
            controls.lblDepartamento.html((oCustomer.InvoiceDepartament == null) ? '' : oCustomer.InvoiceDepartament);
            controls.lblProvincia.html((oCustomer.InvoiceProvince == null) ? '' : oCustomer.InvoiceProvince);
            controls.lblDistrito.html((oCustomer.InvoiceDistrict == null) ? '' : oCustomer.InvoiceDistrict);
            controls.lblCodUbigeo.html((oCustomer.InvoiceUbigeo == null) ? '' : oCustomer.InvoiceUbigeo);
            controls.txtEmail.val((oCustomer.Email == null) ? '' : oCustomer.Email);

        },

        changeDate: function () {

            var that = this,
                controls = this.getControls();

        },

        txtCalendarDate_click: function () {
            var that = this,
                controls = that.getControls();

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

        btnGuardar_click: function () {
            var that = this,
               controls = that.getControls();
        },

        btnConstancia_click: function () {
            var that = this,
              controls = that.getControls();
        }

    };
    $.fn.INTChangeBillingCycle = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTChangeBillingCycle'),
                options = $.extend({}, $.fn.INTChangeBillingCycle.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTChangeBillingCycle', data);
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

    $.fn.INTChangeBillingCycle.defaults = {
    }

    $('#divBody').INTChangeBillingCycle();
})(jQuery);