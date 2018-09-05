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

var vid;

$(document).ready(function () {

    $('.btn-update').click(function () {
        var data = {
            id: vid,
            versionName: $('.versionName2').val(),
            versionCode: $('.versionCode2').val(),
            updateLog: $('.updateLog2').val(),
        }
        jQuery.postNL('../settingAjax/UpdateVersion', data, function (data) {
            $('#modal-edit').modal('hide')
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    changePage(page);
                }
            })
        })
    })

    $('body').delegate('.btn-edit', 'click', function () {
        vid = $(this).parent().data('id')
        $('#modal-edit').modal('show')
        var data1 = {
            Id: vid
        }
        jQuery.postNL('../settingAjax/GetVersionById', data1, function (data) {
            var data = data.ListData[0]
            $('.versionName2').val(data.versionName)
            $('.versionCode2').val(data.versionCode)
            $('.updateLog2').val(data.updateLog)
        })
    })

    $('body').delegate('.btn-del', 'click', function () {
        var id = $(this).parent().data('id')
        var data1 = {
            Id: id
        }
        layer.confirm('确认删除吗？', {
            btn: ['确定', '取消']
        }, function () {
            jQuery.postNL('../settingAjax/DeleteVersion', data1, function (data) {
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

    $('.li-setting').addClass('open')
    $('.a-version').addClass('active')

    $('.btn-save').click(function () {
        $("#myForm").ajaxSubmit(function (res) {
            var data = JSON.parse(res)
            if (data.HttpCode == 200) {
                $('#modal-popout').modal('hide')
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(page)
                    }
                })
            } else {
                layer.msg(data.Message)
            }


            //var h = '<img class="img-show" src="' + res + '" alt="img">';
            //$('.img-show').remove();
            //$('#divImgs').prepend(h);
        });
    })

    changePage(1);
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
        modelType: $('.select-type').val(),
        isEnglish: $('.select-isEnglish').val()
    }

    jQuery.postNL('../settingAjax/GetVersionList', data1, function (data) {

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
            var versionName = item.versionName == null ? "" : item.versionName
            var versionCode = item.versionCode == null ? "" : item.versionCode
            var updateLog = item.updateLog == null ? "" : item.updateLog
            var targetSize = item.targetSize == null ? "" : item.targetSize
            var editTime = item.editTime;
            var id = item.id;


            h += `<tr><td class="center tds td-versionName" title="${versionName}">${versionName}</td>
<td class="center tds td-versionCode" title="${versionCode}">${versionCode}</td>
<td class="center tds td-updateLog" title="${updateLog}">${updateLog}</td>
<td class="center tds td-updateLog" title="${targetSize}">${targetSize}</td>
<td class="center tds td-updateLog" title="${editTime}">${editTime}</td>
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

function GetData() {
    var data = {
        id: vid,
        versionName: $('.versionName').val(),
        versionCode: $('.versionCode').val(),
        updateLog: $('.updateLog').val(),
        apkFile: $('#files').val(),
    }
    return data;
}