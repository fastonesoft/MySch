function DataGridRequest(url, param) {
    //禁用
    $('.easyui-linkbutton').linkbutton('disable');
    //打开窗口
    $.post(url, param, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('.dialog-form').length > 0) $('.dialog-form').remove();
            //加载
            $('<div id="dialog-form" class="dialog-form">').appendTo('#body').html(d);
        }
    });
}

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
            if ($('.dialog-form').length > 0) $('.dialog-form').remove();
            //加载
            $('<div id="dialog-form" class="dialog-form">').appendTo('#body').html(d);
        }
    });
}

function DataGridRowID(gridID, url) {
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
            if ($('.dialog-form').length > 0) $('.dialog-form').remove();
            //加载
            $('<div id="dialog-form" class="dialog-form">').appendTo('#body').html(d);
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
    $.post(url, { entity: row }, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('.dialog-form').length > 0) $('.dialog-form').remove();
            //加载
            $('<div id="dialog-form" class="dialog-form">').appendTo('#body').html(d);
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
            if ($('.dialog-form').length > 0) $('.dialog-form').remove();
            //加载
            $('<div id="dialog-form" class="dialog-form">').appendTo('#body').html(d);
        }
    });
}

function DataGridSearchQuery(textID, gridIDA, gridIDB, urlA, urlB, query) {
    var text = $.trim($(textID).val());
    if (text.length == 0) {
        //清除网格
        $(gridIDA).datagrid('loadData', { total: 0, rows: [] });
        $(gridIDB).datagrid('loadData', { total: 0, rows: [] });
    } else {
        //读取数据
        $(gridIDA).datagrid({ url: urlA, queryParams: query });
        $(gridIDB).datagrid({ url: urlB, queryParams: query });
    }
    //清空查询
    $(textID).val('');
}


function DataGridSearch(textID, gridID, url) {
    var text = $.trim($(textID).val());
    if (text.length == 0) {
        //清除网格
        $(gridID).datagrid('loadData', { total: 0, rows: [] });
    } else {
        //读取数据
        $(gridID).datagrid({ url: url, queryParams: { id: text } });
    }
    //清空查询
    $(textID).val('');
}

function DataGridSearchParam(textID, gridID, url, param) {
    var text = $.trim($(textID).val());
    if (text.length == 0) {
        //清除网格
        $(gridID).datagrid('loadData', { total: 0, rows: [] });
    } else {
        //读取数据
        $(gridID).datagrid({ url: url, queryParams: param });
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
                $(gridID).datagrid({ url: url, queryParams: { id: text } });
            }
            //清空查询
            $(this).val('');
        }
    });
}

function DataGridSearchPressParam(textID, gridID, url, paramName) {
    $(textID).keypress(function (e) {
        if (e.which == 13) {
            var text = $.trim($(this).val());
            if (text.length == 0) {
                //清除网格
                $(gridID).datagrid('loadData', { total: 0, rows: [] });
            } else {
                $(gridID).datagrid({ url: url, queryParams: { paramName: text } });
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
    $(gridID).datagrid({ url: url, queryParams: params });
}


/////////////////////////////////////////////////
//      无窗体动作
////////////////////////////////////////////////
function DataGridRowsNoForm(gridID, url, Success) {
    //选择网格
    var rows = $(gridID).datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('错误提示', '错误：未选定网格数据！', 'error');
        return false;
    }
    //提交数据
    $.post(url, { entitys: rows }, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
        } else {
            //提交成功
            Success(d);
        }
    });
}

function DataGridRowNoForm(gridID, url, Success) {
    //选择网格
    var row = $(gridID).datagrid('getSelected');
    if (!row) {
        $.messager.alert('错误提示', '错误：未选定网格数据！', 'error');
        return false;
    }
    //提交数据
    $.post(url, { id: row.ID }, function (res) {
        if (res.error) {
            $.messager.alert('错误提示', res.message, 'error');
        } else {
            //提交成功
            Success(res);
        }
    });
}




//////////////////////////////////////////////////////////////////////////
//  单独更新
//////////////////////////////////////////////////////////////////////////

//无动作窗口
function DialogShow(title, width, height) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '关闭',
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
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.input-validation-error:first').focus();
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
            $('.input-validation-error:first').focus();
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

function DialogAdds(title, width, height, postUrl, gridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.input-validation-error:first').focus();
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
                        $(gridID).datagrid('loadData', d);
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
            $('.input-validation-error:first').focus();
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
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.input-validation-error:first').focus();
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
            //错误输入聚焦
            $('.input-validation-error:first').focus();
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
            text: '确定',
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

function DialogReload(title, width, height, postUrl, reloadGridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.input-validation-error:first').focus();
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
                        $(reloadGridID).datagrid('reload');
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
            $('.input-validation-error:first').focus();
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

//////////////////////////////////////////////////////////////////////////
//  只显示添加的内容
//////////////////////////////////////////////////////////////////////////

function DialogUpdateGrids(title, width, height, postUrl, reloadGridID, addToGridID) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.input-validation-error:first').focus();
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
                        $(addToGridID).datagrid('loadData', d);
                        $(reloadGridID).datagrid('reload');
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
            $('.input-validation-error:first').focus();
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

function DialogGrid(title, width, height, postUrl, Success) {
    $('#dialog-form').dialog({
        title: title,
        width: width,
        height: height,
        closable: false,
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var form = $('form');
                if (!form.validate().form()) {
                    //错误输入聚焦
                    $('.input-validation-error:first').focus();
                    return false;
                }
                //通过验证，添加
                $.post(postUrl, form.serialize(), function (d) {
                    if (d.error) {
                        $.messager.alert('错误提示', d.message, 'error');
                    } else {
                        //关窗口
                        $('#dialog-form').dialogClose();
                        //成功
                        Success();
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
            $('.input-validation-error:first').focus();
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

