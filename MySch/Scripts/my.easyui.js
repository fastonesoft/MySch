
function DataGridNone(url) {
    //禁用
    $('.easyui-linkbutton').linkbutton('disable');
    //打开窗口
    $.post(url, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('<div id="dialog-form">').length > 0) $('<div id="dialog-form">').remove();
            //加载
            $('<div id="dialog-form">').appendTo('#body').html(d);
        }
    });
}

function DataGridRow(gridID, url) {
    //选择网格
    var row = $(gridID).datagrid('getSelected');
    if (!row) {
        $.messager.alert('错误提示', '错误：未选定网格数据！', 'error');
        return false;
    }
    //禁用
    $('.easyui-linkbutton').linkbutton('disable');
    //打开窗口
    $.post(url, { id: row.ID }, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('<div id="dialog-form">').length > 0) $('<div id="dialog-form">').remove();
            //加载
            $('<div id="dialog-form">').appendTo('#body').html(d);
        }
    });
}

function DataGridRows(gridID, url) {
    //选择网格
    var rows = $(gridID).datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('错误提示', '错误：未选定网格数据！', 'error');
        return false;
    }
    //禁用
    $('.easyui-linkbutton').linkbutton('disable');
    //打开窗口
    $.post(url, { rows: rows }, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('<div id="dialog-form">').length > 0) $('<div id="dialog-form">').remove();
            //加载
            $('<div id="dialog-form">').appendTo('#body').html(d);
        }
    });
}

function DataGridSearch(textID, gridID, url) {
    var text = $.trim($(textID).val());
    if (text.length == 0) {
        //清除网格
        $(gridID).datagrid('loadData', { total: 0, rows: [] });
    } else {
        //读取数据
        $(gridID).datagrid({ url: url, queryParams: { id: text } })
    }
    //清空查询
    $(textID).val('');
}

function DataGridSearchPress(textID, gridID, url) {
    $(textID).keypress(function (e) {
        if (e.which == 13) {
            var text = $.trim($(this).val());
            if (text.length == 0) {
                //清除网格
                $(gridID).datagrid('loadData', { total: 0, rows: [] });
            } else {
                $(gridID).datagrid({ url: url, queryParams: { id: text } })
            }
            //清空查询
            $(this).val('');
        }
    });
}

function DataGridRefresh(gridID, url) {
    $(gridID).datagrid({ url: url });
}

function DataGridParams(gridID, url, params) {
    $(gridID).datagrid({ url: url, queryParams: params })
}

//////////////////////////////////////////////////////////////////////////
//  单独更新
//////////////////////////////////////////////////////////////////////////

function DialogAdd(title, width, height, postUrl, gridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '添加',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.field-validation-error:first').parent().find('input').focus();
                    return false;
                }
                //通过验证，添加
                $.post(postUrl, form.serialize(), function (d) {
                    if (d.error) {
                        $.messager.alert('错误提示', d.message, 'error');
                    } else {
                        //关窗口
                        $('#dialog-form').dialogClose();
                        //更新网格
                        $(gridID).datagrid('appendRow', d);
                    }
                });
            }
        }, {
            text: '取消',
            iconCls: 'icon-no',
            handler: function () {
                $('#dialog-form').dialogClose();
            }
        }],
        onOpen: function () {
            //重置渲染、输入验证、错误聚焦
            var form = $('form').revalidate();
            form.validate().form();
            //错误输入聚焦
            $('.field-validation-error:first').parent().find('input').focus();
        },
        onClose: function () {
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
            //清除提示
            $('div.tooltip').remove();
            $('div.combo-p').remove();
        },
    });
}

function DialogEdit(title, width, height, postUrl, gridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '修改',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.field-validation-error:first').parent().find('input').focus();
                    return false;
                }
                //通过验证，修改
                $.post(postUrl, form.serialize(), function (d) {
                    if (d.error) {
                        $.messager.alert('错误提示', d.message, 'error');
                    } else {
                        //关窗口
                        $('#dialog-form').dialogClose();
                        //更新网格
                        var row = $(gridID).datagrid('getSelected');
                        var index = $(gridID).datagrid('getRowIndex', row);
                        $(gridID).datagrid('updateRow', { index: index, row: d });
                    }
                });
            }
        }, {
            text: '取消',
            iconCls: 'icon-no',
            handler: function () {
                $('#dialog-form').dialogClose();
            }
        }],
        onOpen: function () {
            //重置渲染、输入验证、错误聚焦
            var form = $('form').revalidate();
            form.validate().form();
            //对输入框的焦点变换同样做验证
            $('#dialog-form :text').change(function () {
                var form = $('form').revalidate();
                form.validate().form();
            })
            //错误输入聚焦
            $('.field-validation-error:first').parent().find('input').focus();
        },
        onClose: function () {
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
            //清除提示
            $('div.tooltip').remove();
            $('div.combo-p').remove();
        },
    });
}

function DialogDel(title, width, height, postUrl, gridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '删除',
            iconCls: 'icon-ok',
            handler: function () {
                //直接进行删除操作
                $.post(postUrl, $('form').serialize(), function (d) {
                    if (d.error) {
                        $.messager.alert('错误提示', d.message, 'error');
                    } else {
                        //关窗口
                        $('#dialog-form').dialogClose();
                        //更新网格
                        var row = $(gridID).datagrid('getSelected');
                        var index = $(gridID).datagrid('getRowIndex', row);
                        $(gridID).datagrid('deleteRow', index);
                    }
                });
            }
        }, {
            text: '取消',
            iconCls: 'icon-no',
            handler: function () {
                $('#dialog-form').dialogClose();
            }
        }],
        onClose: function () {
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
            //清除提示
            $('div.tooltip').remove();
            $('div.combo-p').remove();
        },
    });
}

//////////////////////////////////////////////////////////////////////////
//  自动更新
//////////////////////////////////////////////////////////////////////////

function ReDialogAdd(title, width, height, postUrl, gridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '添加',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.field-validation-error:first').parent().find('input').focus();
                    return false;
                }
                //通过验证，添加
                $.post(postUrl, form.serialize(), function (d) {
                    if (d.error) {
                        $.messager.alert('错误提示', d.message, 'error');
                    } else {
                        //关窗口
                        $('#dialog-form').dialogClose();
                        //更新网格
                        $(gridID).datagrid('reload');
                    }
                });
            }
        }, {
            text: '取消',
            iconCls: 'icon-no',
            handler: function () {
                $('#dialog-form').dialogClose();
            }
        }],
        onOpen: function () {
            //重置渲染、输入验证、错误聚焦
            var form = $('form').revalidate();
            form.validate().form();
            //错误输入聚焦
            $('.field-validation-error:first').parent().find('input').focus();
        },
        onClose: function () {
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
            //清除提示
            $('div.tooltip').remove();
            $('div.combo-p').remove();
        },
    });
}

function ReDialogEdit(title, width, height, postUrl, gridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '修改',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.field-validation-error:first').parent().find('input').focus();
                    return false;
                }
                //通过验证，修改
                $.post(postUrl, form.serialize(), function (d) {
                    if (d.error) {
                        $.messager.alert('错误提示', d.message, 'error');
                    } else {
                        //关窗口
                        $('#dialog-form').dialogClose();
                        //更新网格
                        $(gridID).datagrid('reload');
                    }
                });
            }
        }, {
            text: '取消',
            iconCls: 'icon-no',
            handler: function () {
                $('#dialog-form').dialogClose();
            }
        }],
        onOpen: function () {
            //重置渲染、输入验证、错误聚焦
            var form = $('form').revalidate();
            form.validate().form();
            //错误输入聚焦
            $('.field-validation-error:first').parent().find('input').focus();
        },
        onClose: function () {
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
            //清除提示
            $('div.tooltip').remove();
            $('div.combo-p').remove();
        },
    });
}

function ReDialogDel(title, width, height, postUrl, gridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '删除',
            iconCls: 'icon-ok',
            handler: function () {
                //直接进行删除操作
                $.post(postUrl, $('form').serialize(), function (d) {
                    if (d.error) {
                        $.messager.alert('错误提示', d.message, 'error');
                    } else {
                        //关窗口
                        $('#dialog-form').dialogClose();
                        //更新网格
                        $(gridID).datagrid('reload');
                    }
                });
            }
        }, {
            text: '取消',
            iconCls: 'icon-no',
            handler: function () {
                $('#dialog-form').dialogClose();
            }
        }],
        onClose: function () {
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
            //清除提示
            $('div.tooltip').remove();
            $('div.combo-p').remove();
        },
    });
}