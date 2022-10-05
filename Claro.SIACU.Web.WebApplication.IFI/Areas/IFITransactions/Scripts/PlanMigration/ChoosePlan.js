(function ($, undefined) {
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormChoosePlan.defaults, $element.data(), typeof options === 'object' && options);

        this.setControles({
            form: $element
            , btnAddPlan: $('#btnAddPlan', $element)
            , tblPlans: $("#tblPlans", $element)
            , btnClean: $("#btnClean", $element)
            , btnSearchByDescription: $("#btnSearchByDescription", $element)
            , btnSearchByCampaign: $("#btnSearchByCampaign", $element)
            , btnSearchBySolution: $("#btnSearchBySolution", $element)
            , txtSearchByDescription: $("#txtSearchByDescription", $element)
            , sltSearchByCampaign: $("#sltSearchByCampaign", $element)
            //, sltSearchBySolution: $("#sltSearchBySolution", $element)
            , tblPlansBody: $("#tblPlansBody", $element)
            , sltSearchByOptions: $("#sltSearchByOptions", $element)
            , divSltVigencia: $("#divSltVigencia", $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();
            controls.btnClean.addEvent(that, 'click', that.btnClean_Click);
            controls.txtSearchByDescription.addEvent(that, 'keyup', that.txtSearchByDescription_keyup);
            controls.sltSearchByOptions.addEvent(that, 'change', that.sltSearchByOptions_Click);
            controls.sltSearchByCampaign.addEvent(that, 'change', that.btnSearchByCampaign_Click);
            that.LoadPlans();
            that.render();
        },

        LoadPlans: function () {
            var that = this,
                controls = that.getControls(),
                objRequestDataModel = {};
            objRequestDataModel.strIdSession = Session.IDSESSION;
            objRequestDataModel.strCodeUser = Session.USERACCESS.login;
            objRequestDataModel.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestDataModel.strTypeProduct = Session.strProductType;
            objRequestDataModel.strPermitions = Session.USERACCESS.optionPermissions;
            objRequestDataModel.strOffice = global_Office;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestDataModel),
                url: location.protocol + '//' + location.host + '/IFITransactions/PlanMigration/ChoosePlanLoad',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        that.objLteMigrationPlanChoosePlanLoad = response.data;
                    }
                }
            });
        },
        render: function () {
            var that = this,
                controls = this.getControls();

            if (that.objLteMigrationPlanChoosePlanLoad.lstVigencyType != null && that.objLteMigrationPlanChoosePlanLoad.lstVigencyType.length > 0)
                that.getOption(that.objLteMigrationPlanChoosePlanLoad.lstVigencyType);
            if (!that.objLteMigrationPlanChoosePlanLoad.blnPermission) {
                controls.divSltVigencia.css("display", "none");
            }

            if (that.objLteMigrationPlanChoosePlanLoad.lstCampaigns != null && that.objLteMigrationPlanChoosePlanLoad.lstCampaigns.length > 0)
                that.getCampaign(that.objLteMigrationPlanChoosePlanLoad.lstCampaigns.sort());

            that.loadPlansDataTable(that.objLteMigrationPlanChoosePlanLoad.lstPlans, that.objLteMigrationPlanChoosePlanLoad.blnPermission);



        },
        loadPlansDataTable: function (objDataPlans, bolPermition) {
            var that = this,
                controls = this.getControls();


            controls.tblPlans.DataTable({
                "pagingType": "full_numbers",
                "scrollY": "200px",
                "scrollCollapse": true,
                "processing": true,
                "serverSide": false,
                "paging": true,
                "pageLength": 10,
                "destroy": true,
                "searching": true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "loadingRecords": "&nbsp;",
                    "processing": "<img src=" + that.strUrlLogo + " width='25' height='25' /> Cargando ... </div>",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "search": "Busqueda General",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    //"searching": true,
                    "emptyTable": "No existen datos"
                },
                "data": objDataPlans
                ,
                "columns": [
                    { "orderable": false, "data": null, className: "select-radio", "defaultContent": "", render: function (data) { return "&nbsp"; } },
                    { "orderable": true, order: "asc", "data": "strSisactPlanDes" },
                    { "data": "strCampaignDes" },
                    { "data": "strStatus" },
                    { "data": "strSisactPlanCode" },
                    { "data": "strCampaignCode" }
                ],
                "columnDefs": [
                {
                    targets: 3,
                    visible: bolPermition,
                    render: function (data, type, row) {
                        return data == '1' ? 'SI' : 'NO';
                    },

                },
                {
                    targets: 4,
                    visible: false
                },
                {
                    targets: 5,
                    visible: false
                }],
                select: {
                    style: 'os',
                    info: false
                }
            });
            $('.dataTables_filter').hide();

        },
        btnClean_Click: function () {
            var that = this,
                controls = this.getControls();
            controls.txtSearchByDescription.val("");
            that.objLteMigrationPlanChoosePlanLoad.lstCampaignNSolutions = that.objLteMigrationPlanChoosePlanLoad.lstPlans;
            if (that.objLteMigrationPlanChoosePlanLoad.blnPermission)
                controls.sltSearchByOptions.val("-1");
            if (that.objLteMigrationPlanChoosePlanLoad.lstCampaigns != null)
                that.getCampaign(that.objLteMigrationPlanChoosePlanLoad.lstCampaigns);

            that.loadPlansDataTable(that.objLteMigrationPlanChoosePlanLoad.lstPlans, that.objLteMigrationPlanChoosePlanLoad.blnPermission);

        },
        btnSearchByCampaign_Click: function () {
            var that = this,
                controls = this.getControls();
            that.loadPlansDataTable(that.searchObjeto(false, true), that.objLteMigrationPlanChoosePlanLoad.blnPermission);
        },
        txtSearchByDescription_keyup: function () {
            var that = this,
                controls = this.getControls();
            that.loadPlansDataTable(that.searchObjeto(false, false), that.objLteMigrationPlanChoosePlanLoad.blnPermission);
        },
        sltSearchByOptions_Click: function () {
            var that = this,
                controls = this.getControls();
            that.loadPlansDataTable(that.searchObjeto(true, false), that.objLteMigrationPlanChoosePlanLoad.blnPermission);
        },
        getCampaign: function (lstCampaigns) {
            if (lstCampaigns.length > 0) {
                var that = this,
                    controls = that.getControls();
                controls.sltSearchByCampaign.html("");
                controls.sltSearchByCampaign.append($('<option>', { value: '', html: 'Seleccionar' }));
                $.each(lstCampaigns, function (index, value) {
                    controls.sltSearchByCampaign.append($('<option>', { value: value, html: value }));
                });
            }
        },
        getOption: function (lstSearchOptions) {
            if (lstSearchOptions.length > 0) {
                var that = this,
                    controls = that.getControls();
                $.each(lstSearchOptions, function (index, value) {
                    controls.sltSearchByOptions.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        getUnique: function (array, prop) {
            var uniques = [],
                hash = {};

            array.forEach(function (object) {
                var value = object[prop];

                if (!hash[value]) {
                    hash[value] = true;
                    uniques.push(object[prop]);
                }
            });

            return uniques;
        },
        setControles: function (value) {
            this.m_controls = value;
        },
        searchObjeto: function (bolSltOptionValidity, bolSltCampaigns) {
            var that = this,
                controls = this.getControls();
            var objFilter = that.objLteMigrationPlanChoosePlanLoad.lstPlans;
            if (controls.sltSearchByOptions.val() === "VIGENTES")
                objFilter = objFilter.filter(function (x) { return x.strStatus == "1" });
            else if (controls.sltSearchByOptions.val() === "NO VIGENTES")
                objFilter = objFilter.filter(function (x) { return x.strStatus == "0" });

            if (bolSltOptionValidity) {
                if (controls.sltSearchByOptions.val() === "VIGENTES" || controls.sltSearchByOptions.val() === "NO VIGENTES") {
                    var objFilterCampaign = that.getUnique(objFilter, "strCampaignDes").sort();
                    that.getCampaign(objFilterCampaign);
                } else {
                    that.getCampaign(that.objLteMigrationPlanChoosePlanLoad.lstCampaigns);
                }
            } else {
                if (controls.sltSearchByCampaign.val() != "" && controls.sltSearchByCampaign.val() != null) {
                    objFilter = objFilter.filter(function (x) { return x.strCampaignDes == controls.sltSearchByCampaign.val() });
                }

            }
            var strTextFilter = controls.txtSearchByDescription.val();
            if (strTextFilter != "")
                objFilter = objFilter.filter(function (x) { return x.strSisactPlanDes.toUpperCase().indexOf(strTextFilter.toUpperCase()) >= 0 || x.strCampaignDes.toUpperCase().indexOf(strTextFilter.toUpperCase()) >= 0 });

            return objFilter;

        },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        objLteMigrationPlanChoosePlanLoad: {}
    };
    $.fn.FormChoosePlan = function () {
        var option = arguments[0], args = arguments, value, allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormChoosePlan'),
                options = $.extend({}, $.fn.FormChoosePlan.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormChoosePlan', data);
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
    $.fn.FormChoosePlan.defaults = {
    }
    $('#ChoosePlan', $('.modal:last')).FormChoosePlan();

})(jQuery);