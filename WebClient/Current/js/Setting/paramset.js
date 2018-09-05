var orderField = 'id'
var desc = true;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var videoId;

var isAdd = true;

$(document).ready(function () {

    $('body').delegate('.btn-edit', 'click', function () {
        $('#modal-popout').modal('show')
        isAdd = false;
        var id = $(this).parent().data('id')
        videoId = id;
        var data1 = {
            str: id
        }
        jQuery.postNL('../settingAjax/GetParamSetItem', data1, function (data) {
            var data = data.ListData[0]
            $('.key').val(data.key)
            $('.value').val(data.value)
            $('.remark').val(data.remark)
            $('.uuid').val(data.uuid)
            $('.type').val(data.type)
        })
    })

    $('.addModel').click(function () {
        isAdd = true;
        $('.key').val("")
        $('.value').val("")
        $('.remark').val("")
        $('.uuid').val("")
    })

    $('.btn-save').click(function () {
        if (isAdd) {
            jQuery.postNL('../settingAjax/AddParamSet', GetData(), function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page);
                    }
                })
            })
        } else {
            jQuery.postNL('../settingAjax/UpdateParamSet', GetData(), function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page);
                    }
                })
            })
        }

    })

    $('.li-setting').addClass('open')
    $('.a-paramset').addClass('active')

    changePage(1);

    $('#files').change(function (e) {
        var file = e.target.files[0];
        var userData = '{"Vod":{"UserData":"{"IsShowWaterMark":"false","Priority":"7"}"}}';
        uploader.addFile(file, null, null, null, '{"Vod":{"UserData":{"IsShowWaterMark":"false","Priority":"7"}}}');
    })

})

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
        modelType: $('.select-type').val()
    }

    jQuery.postNL('../settingajax/GetParamSet', data1, function (data) {

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
            var key = item.key == null ? "" : item.key
            var value = item.value == null ? "" : item.value
            var remark = item.remark == null ? "" : item.remark
            var uuid = item.uuid == null ? "" : item.uuid
            var type = item.type == null ? "" : item.type
            //var id = item.id; 

            h += `<tr><td class="center tds td-key" title="${key}">${key}</td>
<td class="center tds td-value" title="${value}">${value}</td>
<td class="center tds td-uuid" title="${uuid}">${uuid}</td>
<td class="center tds td-type" title="${type}">${type}</td>
<td class="center tds td-remark" title="${remark}">${remark}</td>
<td class="text-center">
    <div class="btn-group" data-id=${key}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title="修改"><i class="fa fa-pencil"></i></button>

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

function GetData() {
    var data = {
        oldKey: videoId,
        key: $('.key').val(),
        value: $('.value').val(),
        remark: $('.remark').val(),
        uuid: $('.uuid').val(),
        type: $('.type').val()
    }
    return data;
}