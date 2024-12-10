$(function () {

    $("#FormTemplateFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#FormTemplateFilter div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#FormTemplateFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/FormTemplateFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('EasyAbpDynamicForm');

    var service = easyAbp.dynamicForm.formTemplates.formTemplate;
    var createModal = new abp.ModalManager(abp.appPath + 'DynamicForm/FormTemplates/FormTemplate/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'DynamicForm/FormTemplates/FormTemplate/EditModal');

    var dataTable = $('#FormTemplateTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                text: l('Form'),
                                action: function (data) {
                                    document.location.href = document.location.origin + abp.appPath + 'DynamicForm/Forms/Form?formTemplateId=' + data.record.id;
                                }
                            },
                            {
                                text: l('FormItemTemplate'),
                                action: function (data) {
                                    document.location.href = document.location.origin + abp.appPath + 'DynamicForm/FormTemplates/FormItemTemplate?formTemplateId=' + data.record.id;
                                }
                            },
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.DynamicForm.FormTemplate.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.DynamicForm.FormTemplate.Delete'),
                                confirmMessage: function (data) {
                                    return l('FormTemplateDeletionConfirmationMessage', data.record.id);
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
                title: l('FormTemplateFormDefinitionName'),
                data: "formDefinitionName"
            },
            {
                title: l('FormTemplateName'),
                data: "name"
            },
            {
                title: l('FormTemplateCustomTag'),
                data: "customTag"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFormTemplateButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
