//clear
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
    $('body').delegate('.btn-detail', 'click', function () {
        var id = $(this).parent().data('id')
        location.href = '/gym/deviceList?gymid=' + id;
    })

    $('body').delegate('.btn-edit', 'click', function () {
        isAdd = false;
        $('#modal-popout').modal('show')
        var id = $(this).parent().data('id')
        gymId = id;
        var data1 = {
            Id: id
        }
        jQuery.postNL('../gymAjax/GetGymById', data1, function (data) {
            var user = data.ListData[0];
            $('.name').val(user.name)
            $('.UserName').val(user.UId)
            $('.nameE').val(user.nameE)
            $('.UserPwd').val('')
        })
    })

    $('.addModel').click(function () {
        isAdd = true;
        $('.name').val('')
        $('.UserName').val('')
        $('.UserPwd').val('')
        $('.nameE').val('')
    })

    $('.btn-save').click(function () {
        var data1 = GetData();
        if (isAdd) {
            jQuery.postNL('../gymAjax/Addgym', data1, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page)
                    }
                })
            })
        } else {
            jQuery.postNL('../gymAjax/UpdateGym', data1, function (data) {
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

    $('.li-gym').addClass('open')
    $('.a-gymList').addClass('active')

    changePage(1);

    //$('.headImg').click(function () {
    //    $('#upImg').val('');
    //    $('#upImg').click();
    //})

    //$("#upImg").change(function () {
    //    //console.log(1);
    //    var file = this.files[0];
    //    var reader = new FileReader();
    //    reader.readAsDataURL(file);
    //    reader.onload = function (e) {
    //        $('.headImg').attr('src', this.result);
    //        //$(".img_upload_show").attr("src", this.result);
    //        //$(".img_upload_base").val(this.result);
    //    };
    //});

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
        size: $('.select-size').val()
    }

    jQuery.postNL('../gymAjax/GetgymList', data1, function (data) {

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
            var nameE = item.nameE == null ? "" : item.nameE
            var UId = item.UId == null ? "" : item.UId
            var id = item.id;

            h += `<tr><td class="center tds td-id" title="${id}">${id}</td>
<td class="center tds td-name" title="${name}">${name}</td>
<td class="center tds td-nameE" title="${nameE}">${nameE}</td>
<td class="center tds td-nameE" title="${UId}">${UId}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title="编辑"><i class="fa fa-pencil"></i></button>
     

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
        id: gymId,
        name: $('.name').val(),
        nameE: $('.nameE').val(),
        UserName: $('.UserName').val(),
        UserPwd: $('.UserPwd').val(),
    }
    return data;
}
