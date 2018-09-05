var orderField = 'id'
var desc = true;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var modelId;
var isEnglish = 0;

var isAdd = true;

$(document).ready(function () {

    $('.select-type').change(function () {
        changePage(1);
    })
    
    $('body').delegate('.btn-del', 'click', function () {
        var id = $(this).parent().data('id');
        var data = {
            id: id
        }
        layer.confirm('确认删除吗？', {
            btn: ['确定', '取消']
        }, function () {
            jQuery.postNL('../dragModelAjax/DeleteModel', data, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(page)
                    }
                })

            })

            layer.closeAll('dialog');
        }, function () {
            layer.closeAll('dialog');
        })
    })

    $('body').delegate('.btn-delMN', 'click', function () {
        if ($('.div-mn').length > 1)
            $(this).parent().remove();
        else
            layer.msg('至少保留一个')
    })

    $('#addMN').click(function () {
        AddMN();
    })

    $('body').delegate('.btn-edit', 'click', function () {
        isAdd = false;
        $('#saveBtn').html('更新');
        var id = $(this).parent().data('id');
        var data1 = {
            id: id
        }
        jQuery.postNL('../dragModelAjax/GetModelById', data1, function (data) {
            $('#modal-popout').modal('show')
            var data = data.ListData[0];
            $('.modelName').val(data.modelName)
            $('.modelNameE').val(data.modelNameE)
            //$('.m').val(data.m)
            //$('.n').val(data.n)
            $('.modelType').val(data.modelType)
            modelId = data.id;
            changeLabelHtml(data.modelType)
            var listMN = data.listMN;
            clearMNs();
            for (var i = 0; i < listMN.length; i++) {
                if (i != 0) {
                    var h = $('.div-mn:eq(0)').prop("outerHTML");
                    $('.div-mn').last().after(h);
                    $('.m').last().val(listMN[i].m)
                    $('.n').last().val(listMN[i].n)
                } else {
                    $('.m').last().val(listMN[i].m)
                    $('.n').last().val(listMN[i].n)
                }
            }
        })
    })

    $('.modelType').change(function () {
        var type = $('.modelType').val();
        //console.log(type)
        changeLabelHtml(type);
        //switch (type) {
        //    case '0':
        //        //console.log(type)
        //        $('.mType').html('模式变量(米)');
        //        break;
        //    case '1':
        //        $('.mType').html('模式变量(cal)');
        //        break;
        //    case '2':
        //        $('.mType').html('模式变量(秒)');
        //        break;
        //}

        //$('.mType').html();
    })

    $('.li-dragModel').addClass('open')
    //$('.a-userList').addClass('active')

    changePage(1);

    $('.addModel').click(function () {
        isAdd = true;
        isEnglish = 0;
        $('#saveBtn').html('添加');
        $('.modelName').val('');
        $('.modelNameE').val(''),
        clearMNs();
    })
    $('.addModel2').click(function () {
        isAdd = true;
        isEnglish = 1;
        $('#saveBtn').html('添加');
        $('.modelName').val('');
        clearMNs();
    })

    $('body').delegate('.btn-save', 'click', function () {
        var ms = new Array();
        var ns = new Array();
        $('.m').each(function () {
            var m = $(this).val()
            ms.push(m);
        })
        $('.n').each(function () {
            ns.push($(this).val());
        })
        if (isAdd) {
            var data1 = {
                modelName: $('.modelName').val(),
                modelNameE: $('.modelNameE').val(),
                ms: ms,
                ns: ns,
                //isEnglish: isEnglish,
                modelType: $('.modelType').val()
            }
            jQuery.postNL('../DragModelAjax/AddModel', data1, function (data) {
                $('#modal-popout').modal('hide')
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(1)
                    }
                })
            })
        } else {
            var data1 = {
                id: modelId,
                modelName: $('.modelName').val(),
                modelNameE: $('.modelNameE').val(),
                ms: ms,
                ns: ns,
                //isEnglish: isEnglish,
                modelType: $('.modelType').val()
            }
            jQuery.postNL('../DragModelAjax/UpdateModel', data1, function (data) {
                $('#modal-popout').modal('hide')
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(1)
                    }
                })
            })
        }
    })

})

function clearMNs() {
    $('.div-mn').first().nextAll().remove();

    $('.m').val('')
    $('.n').val('');
}

function changeLabelHtml(type) {
    switch (type) {
        case '0':
        case 0:
            $('.mType').html('模式阶段(米)');
            break;
        case '1':
        case 1:
            $('.mType').html('模式阶段(cal)');
            break;
        case '2':
        case 2:
            $('.mType').html('模式阶段(秒)');
            break;
    }
}

function changePage(index) {
    page = index;
    listSort = new Array();

    $('.table th').each(function () {
        listSort.push($(this).attr('class'))
    })
    var data1 = {
        search: $('.input-search').val(),
        pageIndex: index,
        orderField: orderField,
        isDesc: desc,
        size: $('.select-size').val(),
        modelType: $('.select-type').val(),
        isEnglish: $('.select-isEnglish').val()
    }

    jQuery.postNL('../DragModelAjax/GetModelList', data1, function (data) {

        var index = data.index;
        var pages = data.pages;
        var data = data.ListData;
        if (data.length == 0) {

            if ($('.table th').length > 1) {
                ths = $('.table tr:first').html();
            }
            $('.table tr').remove();

            $('.table tbody').append('<tr class="tr-del"><th>没有相关信息</th></tr>')
            $('.pagination').children().remove();
            return;
        }
        var hh = ''
        if ($('.table th').length < 2) {
            hh += `<tr>${ths}</tr>`;
            $('.table thead').append(hh);
        }
        for (var i = 0; i < data.length; i++) {
            var item = data[i]
            var modelName = item.modelName == null ? "" : item.modelName
            var modelNameE = item.modelNameE == null ? "" : item.modelNameE
            var modelType = item.modelType == null ? "" : item.modelType
            var m = item.m == null ? "" : item.m
            var n = item.n == null ? "" : item.n
            var id = item.id;

            //var id = item.id; 
            //<td class="center tds td-m" title="${m}">${m}</td>
            //    <td class="center tds td-n" title="${n}">${n}</td>

            h += `<tr><td class="center tds td-id" title="${id}">${id}</td>
<td class="center tds td-modelName" title="${modelName}">${modelName}</td>
<td class="center tds td-modelName" title="${modelNameE}">${modelNameE}</td>
<td class="center tds td-modelType" title="${modelType}">${modelType}</td>

<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title="修改"><i class="fa fa-pencil"></i></button>
        <button class="btn btn-xs btn-default btn-del" type="button" title="删除"><i class="fa fa-times"></i></button>
    </div>
</td></tr>`
        }
        h += ' </table>'
        $('.tr-del').remove();
        $('.table tbody tr').remove();
        $('.table').append(h);
        $('.pagination').children().remove();
        var h = getPageHtml(pages, index);
        $('.pagination').append(h);

        //tableFixedTest("fixtable")
    })
}

function AddMN() {
    var h = $('.div-mn:eq(0)').prop("outerHTML");
    $('.div-mn').last().after(h);
    $('.m').last().val('')
    $('.n').last().val('')
}

