$(function () {

    var l = abp.localization.getResource('DynamicForm');

    var service = easyAbp.dynamicForm.formTemplates.formTemplate;
    var createModal = new abp.ModalManager(abp.appPath + 'DynamicForm/FormTemplates/FormItemTemplate/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'DynamicForm/FormTemplates/FormItemTemplate/EditModal');

    var dataTable = $('#FormItemTemplateTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(formTemplateId).then(function (result) {
                    callback({
                        recordsTotal: result.formItemTemplates.length,
                        recordsFiltered: result.formItemTemplates.length,
                        data: result.formItemTemplates
                    });
                });
            }
        },
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('DynamicForm.FormTemplate.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('DynamicForm.FormTemplate.Update'),
                                confirmMessage: function (data) {
                                    return l('FormItemTemplateDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.deleteFormItem(data.record.id)
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
                title: l('FormItemTemplateName'),
                data: "name"
            },
            {
                title: l('FormItemTemplateInfoText'),
                data: "tip"
            },
            {
                title: l('FormItemTemplateType'),
                data: "type"
            },
            {
                title: l('FormItemTemplateOptional'),
                data: "optional"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFormItemTemplateButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
