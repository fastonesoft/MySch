
jQuery.fn.extend({

    //对使用ajax方式读取的表单内容重新进行 加载验证，不然无法验证
    revalidate: function () {
        $(this).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($(this));
        return this;
    },

    //easyui
    dialogClose: function () {
        $(this).dialog('close');
        $(this).parent().remove();
        $('.window-shadow, .window-mask').remove();
    },

    //print
    printDom: function () {
        var str = $(this).html();
        var newWindow = window.open("打印窗口", "_blank");
        newWindow.document.write(str);
        newWindow.document.close();
        newWindow.print();
        newWindow.close();
    }
});

//add easyui tabs
easyobj = {
    addTab: function (id, name, url, icon) {
        var tt = $(id);
        if (tt.tabs('exists', name)) {
            tt.tabs('select', name);
        } else {
            //检测是否有权限
            $.post(url, function (d) {
                if (d.error) {
                    $.messager.alert('错误提示', d.message, 'error');
                } else {
                    tt.tabs('add', {
                        title: name,
                        content: d,
                        selected: true,
                        closable: true,
                        iconCls: icon || 'icon-tip',
                        style: {
                            padding: 8
                        }
                    });
                }
            })
        }
    }
}

Date.prototype.format = function (format) {
    if (!format) {
        format = "yyyy-MM-dd hh:mm:ss";
    }
    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(), // day
        "h+": this.getHours(), // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
        "S": this.getMilliseconds()// millisecond
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

//日期
function formatDate(value, format) {
    //空值
    if (!value) { return ""; }
    //格式
    format = format || "yyyy-MM-dd hh:mm:ss";
    return (new Date(parseInt(value.substring(value.indexOf('(') + 1, value.indexOf(')'))))).format(format);
}

//防止unobtrusive里面变误删除（在这里没有用，拷贝过去才行）
function onError(error, inputElement) {  // 'this' is the form element
    var container = $(this).find("[data-valmsg-for='" + escapeAttributeValue(inputElement[0].name) + "']"),
        replaceAttrValue = container.attr("data-valmsg-replace"),
        replace = replaceAttrValue ? $.parseJSON(replaceAttrValue) !== false : null;

    container.removeClass("field-validation-valid").addClass("field-validation-error");
    error.data("unobtrusiveContainer", container);

    if (replace) {
        //原始
        container.empty();
        error.removeClass("input-validation-error").appendTo(container);

        //添加：提示；前提：必须是窗口输入控件
        if ($(inputElement).hasClass('textbox-text')) {
            //错误：提示
            if (error.html() != "") {
                //错误：添加样式
                $(inputElement).addClass('validatebox-invalid').parent().addClass('validatebox-invalid');
                //错误：添加提示
                $(inputElement).tooltip({
                    //track: true,
                    position: 'right',
                    content: error.text(),
                    hideDelay: 1000,
                    onShow: function () {
                        $(this).tooltip('tip').css({
                            color: '#000',
                            borderColor: '#CC9933',
                            backgroundColor: '#FFFFCC'
                        });
                    },
                });
            } else {
                //正确：去除样式
                $(inputElement).removeClass('validatebox-invalid').parent().removeClass('validatebox-invalid');
                //正确：销毁提示
                $(inputElement).tooltip('destroy');
            }
        }
    }
    else {
        error.hide();
    }
}
