(function ($, undefined) {

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormChooseCoreServicesByPlan.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , tblChooseServicesByPlanCable: $("#tblChooseCoreServicesByPlanCable", $element)
          , tblChooseServicesByPlanInternet: $("#tblChooseCoreServicesByPlanInternet", $element)
          , tblChooseServicesByPlanPhone: $("#tblChooseCoreServicesByPlanPhone", $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();
            that.ChooseCoreServicesLoad();
            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();
            that.loadServicesCoreByCable();
            that.loadServicesCoreByInternet();
            that.loadServicesCoreByTelephone();
            if (that.objChooseCoreServices.lstServicesByPlanCable != null)
                $('#tblChooseCoreServicesByPlanCable tr').find('input:radio:first').click();

            if (that.objChooseCoreServices.lstServicesByPlanInternet != null)
                $('#tblChooseCoreServicesByPlanInternet tr').find('input:radio:first').click();

            if (that.objChooseCoreServices.lstServicesByPlanTelephone != null)
                $('#tblChooseCoreServicesByPlanPhone tr').find('input:radio:first').click();

        },
        loadServicesCoreByCable: function () {
            var that = this,
                controls = this.getControls();
            controls.tblChooseServicesByPlanCable.DataTable({
                "pagingType": "full_numbers",
                "scrollCollapse": false,
                "paging": false,
                "pageLength": 10,
                "sort": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "emptyTable": "No existen datos"
                },
                "data": that.objChooseCoreServices.lstServicesByPlanCable,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "<input type='radio' style='display:none' />"; } },
                    { "data": "SisactServDes" },
                    { "data": "CfWithIgv" },
                    { "data": "SisactServCode" },
                    { "data": "ServiceTypeDes" },
                    { "data": "EquipmentDes" },
                    { "data": "SnCode" },
                    { "data": "SpCode" },
                    { "data": "EquipmentQty" },
                    { "data": "ServiceGroupCode" },
                    { "data": "EquipTypeCode" },
                    { "data": "ServiceGroupDes" },
                    { "data": "ServiceTypeCode" },
                    { "data": "EquipmentType" },
                    { "data": "TmCode" },
                    { "data": "IdEquipment" }
                ],
                "columnDefs": [
                    { orderable: false, className: 'select-radio', targets: 0 },

                    { visible: false, targets: 3 },
                    { visible: false, targets: 4, width: 0 },
                    { visible: false, targets: 5, width: 0 },
                    { visible: false, targets: 6, width: 0 },
                    { visible: false, targets: 7, width: 0 },
                    { visible: false, targets: 8, width: 0 },
                    { visible: false, targets: 9, width: 0 },
                    { visible: false, targets: 10, width: 0 },
                    { visible: false, targets: 11, width: 0 },
                    { visible: false, targets: 12, width: 0 },
                    { visible: false, targets: 13, width: 0 },
                    { visible: false, targets: 14, width: 0 },
                    { visible: false, targets: 15, width: 0 }
                ],
                select: {
                    style: 'os',
                    info: false
                }
            });
        },
        loadServicesCoreByInternet: function () {
            var that = this,
                controls = this.getControls();
            controls.tblChooseServicesByPlanInternet.DataTable({
                "pagingType": "full_numbers",
                "scrollCollapse": false,
                "paging": false,
                "pageLength": 10,
                "sort": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "emptyTable": "No existen datos"
                },
                "data": that.objChooseCoreServices.lstServicesByPlanInternet,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "<input type='radio' style='display:none' />"; } },

                    { "data": "SisactServDes" },
                    { "data": "CfWithIgv" },
                    { "data": "SisactServCode" },
                    { "data": "ServiceTypeDes" },
                    { "data": "EquipmentDes" },
                    { "data": "SnCode" },
                    { "data": "SpCode" },
                    { "data": "EquipmentQty" },
                    { "data": "ServiceGroupCode" },
                    { "data": "EquipTypeCode" },
                    { "data": "ServiceGroupDes" },
                    { "data": "ServiceTypeCode" },
                    { "data": "EquipmentType" },
                    { "data": "TmCode" },
                    { "data": "IdEquipment" }
                ],
                "columnDefs": [
                    { orderable: false, className: 'select-radio', targets: 0 },
                    { visible: false, targets: 3 },
                    { visible: false, targets: 4, width: 0 },
                    { visible: false, targets: 5, width: 0 },
                    { visible: false, targets: 6, width: 0 },
                    { visible: false, targets: 7, width: 0 },
                    { visible: false, targets: 8, width: 0 },
                    { visible: false, targets: 9, width: 0 },
                    { visible: false, targets: 10, width: 0 },
                    { visible: false, targets: 11, width: 0 },
                    { visible: false, targets: 12, width: 0 },
                    { visible: false, targets: 13, width: 0 },
                    { visible: false, targets: 14, width: 0 },
                    { visible: false, targets: 15, width: 0 }
                ],
                select: {
                    style: 'os',
                    info: false
                }
            });

        },
        loadServicesCoreByTelephone: function () {
            var that = this,
                controls = this.getControls();
            controls.tblChooseServicesByPlanPhone.DataTable({
                "pagingType": "full_numbers",
                "scrollCollapse": false,
                "paging": false,
                "pageLength": 10,
                "sort": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "emptyTable": "No existen datos"
                },
                "data": that.objChooseCoreServices.lstServicesByPlanTelephone,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "<input type='radio' style='display:none' />"; } },
                    { "data": "SisactServDes" },
                    { "data": "CfWithIgv" },
                    { "data": "SisactServCode" },
                    { "data": "ServiceTypeDes" },
                    { "data": "EquipmentDes" },
                    { "data": "SnCode" },
                    { "data": "SpCode" },
                    { "data": "EquipmentQty" },
                    { "data": "ServiceGroupCode" },
                    { "data": "EquipTypeCode" },
                    { "data": "ServiceGroupDes" },
                    { "data": "ServiceTypeCode" },
                    { "data": "EquipmentType" },
                    { "data": "TmCode" },
                    { "data": "IdEquipment" }
                ],
                "columnDefs": [
                    { orderable: false, className: 'select-radio', targets: 0 },
                    { visible: false, targets: 3 },
                    { visible: false, targets: 4, width: 0 },
                    { visible: false, targets: 5, width: 0 },
                    { visible: false, targets: 6, width: 0 },
                    { visible: false, targets: 7, width: 0 },
                    { visible: false, targets: 8, width: 0 },
                    { visible: false, targets: 9, width: 0 },
                    { visible: false, targets: 10, width: 0 },
                    { visible: false, targets: 11, width: 0 },
                    { visible: false, targets: 12, width: 0 },
                    { visible: false, targets: 13, width: 0 },
                    { visible: false, targets: 14, width: 0 },
                    { visible: false, targets: 15, width: 0 }
                ],
                select: {
                    style: 'os',
                    info: false
                }
            });
        },

        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getLoadDetailVisualizeCall: function () {
            var controls = this.getControls();

        },
        ChooseCoreServicesLoad: function () {
            var that = this,
                controls = that.getControls(),
                objInputParameters = {};
            objInputParameters.strIdSession = Session.IDSESSION;
            objInputParameters.strIdContract = Session.DATACUSTOMER.ContractID;
            objInputParameters.strProductType = Session.strProductType;
            objInputParameters.strIdPlan = Session.idPlan;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objInputParameters),
                url: location.protocol + '//' + location.host + '/IFITransactions/PlanMigration/ChooseCoreServicesLoad',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        that.objChooseCoreServices = response.data;
                    }
                }
            });

            $.each(that.objChooseCoreServices.lstServicesByPlanCable, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIGVPercentUnity) * 100) / 100;
            })

            $.each(that.objChooseCoreServices.lstServicesByPlanInternet, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIGVPercentUnity) * 100) / 100;
            })

            $.each(that.objChooseCoreServices.lstServicesByPlanTelephone, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIGVPercentUnity) * 100) / 100;
            })
        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        objChooseCoreServices: {}
    };
    $.fn.FormChooseCoreServicesByPlan = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormChooseCoreServicesByPlan'),
                options = $.extend({}, $.fn.FormChooseCoreServicesByPlan.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormChooseCoreServicesByPlan', data);
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
    $.fn.FormChooseCoreServicesByPlan.defaults = {
    }
    $('#ChooseCoreServicesByPlan', $('.modal:last')).FormChooseCoreServicesByPlan();
})(jQuery);