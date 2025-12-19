$(function () {

    $("#FormFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#FormFilter div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#FormFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/FormFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('EasyAbpDynamicForm');

    var service = easyAbp.dynamicForm.forms.form;
    var createModal = new abp.ModalManager(abp.appPath + 'DynamicForm/Forms/Form/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'DynamicForm/Forms/Form/EditModal');

    var dataTable = $('#FormTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList,getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.DynamicForm.Form.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.DynamicForm.Form.Delete'),
                                confirmMessage: function (data) {
                                    return l('FormDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('FormCreationTime'),
                data: "creationTime",
                dataFormat: 'datetime'
            },
            {
                title: l('FormCreator'),
                data: "creatorId"
            },
            {
                title: l('FormFormTemplateName'),
                data: "formTemplateName"
            },
            {
                title: l('FormFormDefinitionName'), // todo: use display name
                data: "formDefinitionName"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFormButton').click(function (e) {
        e.preventDefault();
        createModal.open({ formTemplateId: formTemplateId });
    });
});
