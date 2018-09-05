var orderField = 'id'
var desc = true;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var isAdd = true;

var gymId = 0;

$(document).ready(function () {

    $('body').delegate('.btn-edit', 'click', function () {
        isAdd = false;
        $('#modal-popout').modal('show')
        var id = $(this).parent().data('id')
        gymId = id;
        var data1 = {
            Id: id
        }
        jQuery.postNL('../gymAjax/GetDeviceById', data1, function (data) {
            var user = data.ListData[0];
            $('.name').val(user.name)
            $('.gymId').val(user.gymId)
            //$('.nameE').val(user.nameE)
        })
    })

    $('.addModel').click(function () {
        isAdd = true;
        $('.name').val('')
        $('.nameE').val('')
        //$('.gymId').val(0)
    })

    $('.btn-save').click(function () {
        var data1 = GetData();
        if (isAdd) {
            jQuery.postNL('../gymAjax/AddDevice', data1, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page)
                    }
                })
            })
        } else {
            jQuery.postNL('../gymAjax/UpdateDevice', data1, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page)
                    }
                })
            })
        }
    })

    $('.li-gym2').addClass('open')
    $('.a-deviceList').addClass('active')

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
        //gymId: GymId
    }

    jQuery.postNL('../gymAjax/GetDeviceList', data1, function (data) {

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
            var id = item.id == null ? "" : item.id
            var name = item.name == null ? "" : item.name
            var gymName = item.gymName == null ? "未选择" : item.gymName
            //var nameE = item.nameE == null ? "" : item.nameE
            var id = item.id;

            h += `<tr><td class="center tds td-id" title="${id}">${id}</td>
<td class="center tds td-name" title="${name}">${name}</td>
<td class="center tds td-name" title="${gymName}">${gymName}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title=""><i class="fa fa-pencil"></i></button>
        <button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>
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
    })
}

function GetData() {
    var data = {
        id: gymId,
        name: $('.name').val(),
        //gymId: $('.gymId').val()
        //nameE: $('.nameE').val()
    }
    return data;
}
