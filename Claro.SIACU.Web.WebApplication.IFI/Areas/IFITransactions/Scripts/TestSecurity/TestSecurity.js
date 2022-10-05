(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.QSecurityForm.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element


        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();

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
                url: '/IFITransactions/ServiceLock/GetPermisionLockService',
                async: false,
                error: function (data) {
                    alert("Error JS : en llamar al GetPermisionLockService.", "Alerta");
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
            $("#cbDocumentType option:selected").text(Session.DATACUSTOMER.TipoDocumento)
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
            controls.txtPhone.prop("disabled", true);
            controls.txtParient.prop("disabled", true);
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
                controls.txtOtherRelation.show();
                controls.cbTitularRelation.show();
                controls.txtPhone.hide();
                controls.txtParient.hide();
                controls.lblTitularRelation.show();
                controls.lblOtherRelation.show();
                controls.lblPhone.hide();
                controls.lblParient.hide();

            } else if (value == "3") {
                that.cleanSolicitante();
                that.enableSolicitante();
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
        ddlMotive_change: function () {
            if ($("#ddlMotive option:selected").val() != "-1") {
                $("#cbotype *").prop("disabled", false);
            } else {
                $("#pnlSolicitante *").prop("disabled", true);

            }
        },
        btnSearchIMEI_Click: function () {
            var that = this;
            var controls = that.getControls();
            controls.exampleModal.modal('show');

        },
        chkBloqueo_Click: function () {
            var that = this;
            var controls = that.getControls();
            if (controls.chkBloqueo.is(':checked')) {
                $("#idBloqueoLineaPanel").show(1000);
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

            that.IniLoadPage();
            that.getCodeCustomerType();
            that.getCodeModality();
            that.TestSecurity();
            $("#lblTypeCustomer").html("Tipo de Cliente: "+Session.CLIENTE.CustomerType);


        },
        getCodeCustomerType: function () {
            var that = this,
              controls = that.getControls(),
              objStateType = {};

            objStateType.strIdSession = Session.IDSESSION;
            objStateType.strCustomerType = Session.CLIENTE.CustomerType;

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objStateType),
                url: '/IFITransactions/ServiceLock/GetCodeCustomerType',

                success: function (response) {
                    that.codeCustomerType = "4";
                }
            });
        },
        getCodeModality: function () {
            var that = this,
              controls = that.getControls(),
              objStateType = {};

            objStateType.strIdSession = Session.IDSESSION;
            objStateType.strModality = Session.CLIENTE.Modality;


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(objStateType),
                url: '/IFITransactions/ServiceLock/GetCodeModality',

                success: function (response) {
                    that.codeModality = "1";
                }
            });
        },
        codeModality: "",
        codeCustomerType: "",
        TestSecurity: function () {
            var that = this,
               controls = that.getControls(),
               objStateType = {};

            objStateType.strIdSession = Session.IDSESSION;
            objStateType.strCodeCustomerType = that.codeCustomerType;
            objStateType.strCodeModality = that.codeModality;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objStateType),
                url: '/IFITransactions/ServiceLock/TestSecurity',
                async: false,
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
                    if (response.data != null) {
                        if (response.data.lstTestSecurity != null) {
                            $.each(response.data.lstTestSecurity, function (key, value) {
                              
                                var descrip = "<div class='row'>";
                                descrip += " <div class='col-sm-6'><label>" + value.desQues + "  <span class='text-right label-red-service'>*</span></label></div>";

                                descrip += " <div class='col-sm-6'><select class='form-control select-list' id='idCbo" + value.codQues + "' name='nmCbo" + value.codQues + "'>";
                                descrip += "<option value=''> Seleccionar </option>";
                                $.each(value.ListAnsw, function (key, value) {

                                    descrip += "<option value='" + value.desAns + "'>" + value.desAns + "</option>";
                                   

                                });
                                descrip += "</select></div></div> "
                                $('#ContenedorPreguntas').append(descrip);
                            });

                            $("#ServiceLockForm").ServiceLockForm("setListTest",
                                {
                                    paramListTest: response.data.lstTestSecurity

                                });
                        }
                       

                    }
                }
            });
        },
        canLock: false,
        getDocumentType: function () {
            var that = this,
                controls = that.getControls(),
                objStateType = {};

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
                            controls.cbDocumentType.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        lockService: function () {
            var that = this;
            var controls = that.getControls();
            try {



                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(objParams),
                    url: '/IFITransactions/ServiceLock/SaveTransaction',
                    error: function (ex) {
                        alert("Ha ocurrido un problema en la transacción, por favor volver a intentarlo más tarde.", "Alerta");
                    },
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



                        if (response.Result) {
                            controls.btnSave.prop('disabled', true);
                            controls.btnConstancy.prop('disabled', false);



                        }
                    }
                });
            } catch (e) {
                alert('Ocurrió un error al guardar en la transacción.', "Alerta");
            }
        },
        btnSave_click: function () {

            var that = this;
            var controls = that.getControls();
            
            if (that.canLock == false) {
                ValidateUserIFI('strKeyPerfBloquearServicioIFI', that.lockService, null, null, null);
            } else {
                that.lockService();
            }



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
                            if (stroptionPermissions.indexOf(value.Code2) > 0) {
                                controls.ddlMotive.append($('<option>', { value: value.Code2, html: value.Description }));
                            }

                        });
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
            $("#lblTitle").text("Bloqueo de Servicio");

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

            $('#content').css({ 'height': 538 + 'px' });
        }
    },


    $.fn.QSecurityForm = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('QSecurityForm'),
                options = $.extend({}, $.fn.QSecurityForm.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('QSecurityForm', data);
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
    $.fn.QSecurityForm.defaults = {
    }
    $('#QSecurity').QSecurityForm();
})(jQuery);