if (!Element.prototype.matches) {
  Element.prototype.matches =
    Element.prototype.msMatchesSelector ||
    Element.prototype.webkitMatchesSelector;
}

if (!Element.prototype.closest) {
  Element.prototype.closest = function (s) {
    var el = this;

    do {
      if (el.matches(s)) return el;
      el = el.parentElement || el.parentNode;
    } while (el !== null && el.nodeType === 1);
    return null;
  };
}

var resolveCallbacks = [];
var rejectCallbacks = [];

window.HtmlEditor = {
    throttle: function (callback, delay) {
        var timeout = null;
        return function () {
            var args = arguments;
            var ctx = this;
            if (!timeout) {
                timeout = setTimeout(function () {
                    callback.apply(ctx, args);
                    timeout = null;
                }, delay);
            }
        };
    },
    mask: function (id, mask, pattern, characterPattern) {
      var el = document.getElementById(id);
      if (el) {
          var format = function (value, mask, pattern, characterPattern) {
              var chars = !characterPattern ? value.replace(new RegExp(pattern, "g"), "").split('') : value.match(new RegExp(characterPattern, "g"));
              var count = 0;

              var formatted = '';
              for (var i = 0; i < mask.length; i++) {
                  const c = mask[i];
                  if (chars && chars[count]) {
                      if (/\*/.test(c)) {
                          formatted += chars[count];
                          count++;
                      } else {
                          formatted += c;
                      }
                  }
              }
              return formatted;
          }
          el.value = format(el.value, mask, pattern, characterPattern);
      }
  },
  addContextMenu: function (id, ref) {
     var el = document.getElementById(id);
     if (el) {
        var handler = function (e) {
            e.stopPropagation();
            e.preventDefault(); 
            ref.invokeMethodAsync('HtmlEditorComponentBase.RaiseContextMenu',
                {
                    ClientX: e.clientX,
                    ClientY: e.clientY,
                    ScreenX: e.screenX,
                    ScreenY: e.screenY,
                    AltKey: e.altKey,
                    ShiftKey: e.shiftKey,
                    CtrlKey: e.ctrlKey,
                    MetaKey: e.metaKey,
                    Button: e.button,
                    Buttons: e.buttons,
                });
            return false;
        };
        HtmlEditor[id + 'contextmenu'] = handler;
        el.addEventListener('contextmenu', handler, false);
     }
  },
  addMouseEnter: function (id, ref) {
     var el = document.getElementById(id);
     if (el) {
        var handler = function (e) {
            ref.invokeMethodAsync('HtmlEditorComponentBase.RaiseMouseEnter');
        };
        HtmlEditor[id + 'mouseenter'] = handler;
        el.addEventListener('mouseenter', handler, false);
     }
  },
  addMouseLeave: function (id, ref) {
     var el = document.getElementById(id);
     if (el) {
        var handler = function (e) {
            ref.invokeMethodAsync('HtmlEditorComponentBase.RaiseMouseLeave');;
        };
        HtmlEditor[id + 'mouseleave'] = handler;
        el.addEventListener('mouseleave', handler, false);
     }
  },
  removeContextMenu: function (id) {
      var el = document.getElementById(id);
      if (el && HtmlEditor[id + 'contextmenu']) {
          el.removeEventListener('contextmenu', HtmlEditor[id + 'contextmenu']);
      }
  },
  removeMouseEnter: function (id) {
      var el = document.getElementById(id);
      if (el && HtmlEditor[id + 'mouseenter']) {
          el.removeEventListener('mouseenter', HtmlEditor[id + 'mouseenter']);
      }
  },
  removeMouseLeave: function (id) {
      var el = document.getElementById(id);
      if (el && HtmlEditor[id + 'mouseleave']) {
          el.removeEventListener('mouseleave', HtmlEditor[id + 'mouseleave']);
      }
  },
  preventDefaultAndStopPropagation: function (e) {
    e.preventDefault();
    e.stopPropagation();
  },
  preventArrows: function (el) {
    var preventDefault = function (e) {
      if (e.keyCode === 38 || e.keyCode === 40) {
        e.preventDefault();
        return false;
      }
    };
    if (el) {
       el.addEventListener('keydown', preventDefault, false);
    }
  },
  focusElement: function (elementId) {
    var el = document.getElementById(elementId);
    if (el) {
      el.focus();
    }
  },
  uploadInputChange: function (e, url, auto, multiple, clear, parameterName) {
      if (auto) {
          HtmlEditor.upload(e.target, url, multiple, clear, parameterName);
          e.target.value = '';
      } else {
          HtmlEditor.uploadChange(e.target);
      }
  },
  uploads: function (uploadComponent, id) {
    if (!HtmlEditor.uploadComponents) {
      HtmlEditor.uploadComponents = {};
    }
    HtmlEditor.uploadComponents[id] = uploadComponent;
  },
  uploadChange: function (fileInput) {
    var files = [];
    for (var i = 0; i < fileInput.files.length; i++) {
      var file = fileInput.files[i];
      files.push({
        Name: file.name,
        Size: file.size,
        Url: URL.createObjectURL(file)
      });
    }

    var uploadComponent =
      HtmlEditor.uploadComponents && HtmlEditor.uploadComponents[fileInput.id];
    if (uploadComponent) {
      if (uploadComponent.localFiles) {
        // Clear any previously created preview URL(s)
        for (var i = 0; i < uploadComponent.localFiles.length; i++) {
          var file = uploadComponent.localFiles[i];
          if (file.Url) {
            URL.revokeObjectURL(file.Url);
          }
        }
      }

      uploadComponent.files = Array.from(fileInput.files);
      uploadComponent.localFiles = files;
      uploadComponent.invokeMethodAsync('HtmlEditorUpload.OnChange', files);
    }

    for (var i = 0; i < fileInput.files.length; i++) {
      var file = fileInput.files[i];
      if (file.Url) {
        URL.revokeObjectURL(file.Url);
      }
    }
  },
  removeFileFromUpload: function (fileInput, name) {
    var uploadComponent = HtmlEditor.uploadComponents && HtmlEditor.uploadComponents[fileInput.id];
    if (!uploadComponent) return;
    var file = uploadComponent.files.find(function (f) { return f.name == name; })
    if (!file) { return; }
    var localFile = uploadComponent.localFiles.find(function (f) { return f.Name == name; });
    if (localFile) {
      URL.revokeObjectURL(localFile.Url);
    }
    var index = uploadComponent.files.indexOf(file)
    if (index != -1) {
        uploadComponent.files.splice(index, 1);
    }
    fileInput.value = '';
  },
  removeFileFromFileInput: function (fileInput) {
    fileInput.value = '';
  },
    upload: function (fileInput, url, multiple, clear, parameterName) {
    var uploadComponent = HtmlEditor.uploadComponents && HtmlEditor.uploadComponents[fileInput.id];
    if (!uploadComponent) { return; }
    if (!uploadComponent.files || clear) {
        uploadComponent.files = Array.from(fileInput.files);
    }
    var data = new FormData();
    var files = [];
    var cancelled = false;
    for (var i = 0; i < uploadComponent.files.length; i++) {
      var file = uploadComponent.files[i];
      data.append(parameterName || (multiple ? 'files' : 'file'), file, file.name);
      files.push({Name: file.name, Size: file.size});
    }
    var xhr = new XMLHttpRequest();
    xhr.upload.onprogress = function (e) {
      if (e.lengthComputable) {
        var uploadComponent =
          HtmlEditor.uploadComponents && HtmlEditor.uploadComponents[fileInput.id];
        if (uploadComponent) {
          var progress = parseInt((e.loaded / e.total) * 100);
          uploadComponent.invokeMethodAsync(
            'HtmlEditorUpload.OnProgress',
            progress,
            e.loaded,
            e.total,
            files,
            cancelled
          ).then(function (cancel) {
              if (cancel) {
                  cancelled = true;
                  xhr.abort();
              }
          });
        }
      }
    };
    xhr.onreadystatechange = function (e) {
      if (xhr.readyState === XMLHttpRequest.DONE) {
        var status = xhr.status;
        var uploadComponent =
          HtmlEditor.uploadComponents && HtmlEditor.uploadComponents[fileInput.id];
        if (uploadComponent) {
          if (status === 0 || (status >= 200 && status < 400)) {
            uploadComponent.invokeMethodAsync(
              'HtmlEditorUpload.OnComplete',
                xhr.responseText,
                cancelled
            );
          } else {
            uploadComponent.invokeMethodAsync(
              'HtmlEditorUpload.OnError',
              xhr.responseText
            );
          }
        }
      }
    };
    uploadComponent.invokeMethodAsync('GetHeaders').then(function (headers) {
      xhr.open('POST', url, true);
      for (var name in headers) {
        xhr.setRequestHeader(name, headers[name]);
      }
      xhr.send(data);
    });
  },
  getCookie: function (name) {
    var value = '; ' + decodeURIComponent(document.cookie);
    var parts = value.split('; ' + name + '=');
    if (parts.length == 2) return parts.pop().split(';').shift();
  },
  getCulture: function () {
    var cultureCookie = HtmlEditor.getCookie('.AspNetCore.Culture');
    var uiCulture = cultureCookie
      ? cultureCookie.split('|').pop().split('=').pop()
      : null;
    return uiCulture || 'en-US';
  },
  numericOnPaste: function (e, min, max) {
    if (e.clipboardData) {
      var value = e.clipboardData.getData('text');

      if (value && !isNaN(+value)) {
        var numericValue = +value;
        if (min != null && numericValue >= min) {
            return;
        }
        if (max != null && numericValue <= max) {
            return;
        }
      }

      e.preventDefault();
    }
  },
  numericOnInput: function (e, min, max) {
      var value = e.target.value;

      if (value && !isNaN(+value)) {
        var numericValue = +value;
        if (min != null && !isNaN(+min) && numericValue < min) {
            e.target.value = min;
        }
        if (max != null && !isNaN(+max) && numericValue > max) {
            e.target.value = max;
        }
      }
  },
  numericKeyPress: function (e, isInteger) {
    if (
      e.metaKey ||
      e.ctrlKey ||
      e.keyCode == 9 ||
      e.keyCode == 8 ||
      e.keyCode == 13
    ) {
      return;
    }

    var ch = String.fromCharCode(e.charCode);

    if ((isInteger ? /^[-\d]$/ : /^[-\d,.]$/).test(ch)) {
      return;
    }

    e.preventDefault();
  },
  openContextMenu: function (x,y,id) {
    HtmlEditor.closePopup(id);

    HtmlEditor.openPopup(null, id, false, null, x, y);
  },
 
  findPopup: function (id) {
    var popups = [];
    for (var i = 0; i < document.body.children.length; i++) {
      if (document.body.children[i].id == id) {
        popups.push(document.body.children[i]);
      }
    }
    return popups;
  },
  repositionPopup: function (parent, id) {
      var popup = document.getElementById(id);
      if (!popup) return;

      var rect = popup.getBoundingClientRect();
      var parentRect = parent ? parent.getBoundingClientRect() : { top: 0, bottom: 0, left: 0, right: 0, width: 0, height: 0 };

      if (/Edge/.test(navigator.userAgent)) {
          var scrollTop = document.body.scrollTop;
      } else {
          var scrollTop = document.documentElement.scrollTop;
      }

      var top = parentRect.bottom + scrollTop;

      if (top + rect.height > window.innerHeight && parentRect.top > rect.height) {
          top = parentRect.top - rect.height + scrollTop;
      }

      popup.style.top = top + 'px';
  },
  openPopup: function (parent, id, syncWidth, position, x, y, instance, callback, closeOnDocumentClick = true) {
    var popup = document.getElementById(id);
    if (!popup) return;

    HtmlEditor.activeElement = document.activeElement;

    var parentRect = parent ? parent.getBoundingClientRect() : { top: y || 0, bottom: 0, left: x || 0, right: 0, width: 0, height: 0 };

    if (/Edge/.test(navigator.userAgent)) {
      var scrollLeft = document.body.scrollLeft;
      var scrollTop = document.body.scrollTop;
    } else {
      var scrollLeft = document.documentElement.scrollLeft;
      var scrollTop = document.documentElement.scrollTop;
    }

    var top = y ? y : parentRect.bottom;
    var left = x ? x : parentRect.left;

      if (syncWidth) {
        popup.style.width = parentRect.width + 'px';
        if (!popup.style.minWidth) {
            popup.minWidth = true;
            popup.style.minWidth = parentRect.width + 'px';
        }
    }

    if (window.chrome) {
        var closestFrozenCell = popup.closest('.html-editor-frozen-cell');
        if (closestFrozenCell) {
            HtmlEditor[id + 'FZL'] = { cell: closestFrozenCell, left: closestFrozenCell.style.left };
            closestFrozenCell.style.left = '';
        }
    }

    popup.style.display = 'block';

    var rect = popup.getBoundingClientRect();

    var smartPosition = !position || position == 'bottom';

    if (smartPosition && top + rect.height > window.innerHeight && parentRect.top > rect.height) {
      top = parentRect.top - rect.height;

    
    }

    if (smartPosition && left + rect.width > window.innerWidth && window.innerWidth > rect.width) {
      left = window.innerWidth - rect.width;

      }
    

    if (smartPosition) {
      if (position) {
        top = top + 20;
      }
    }

    if (position == 'left') {
      left = parentRect.left - rect.width - 5;
      top =  parentRect.top;
    }

    if (position == 'right') {
      left = parentRect.right + 10;
      top = parentRect.top;
    }

    if (position == 'top') {
      top = parentRect.top - rect.height + 5;
      left = parentRect.left;
    }

    popup.style.zIndex = 2000;
    popup.style.left = left + scrollLeft + 'px';
    popup.style.top = top + scrollTop + 'px';

    if (!popup.classList.contains('.html-editor-overlaypanel')) {
        popup.classList.add('.html-editor-popup');
    }

    HtmlEditor[id] = function (e) {
        if(e.type == 'contextmenu' || !e.target || !closeOnDocumentClick) return;
        if (!/Android/i.test(navigator.userAgent) && e.type == 'resize') {
            HtmlEditor.closePopup(id, instance, callback);
            return;
        }
        var closestPopup = e.target.closest('.html-editor-popup');
        if (closestPopup && closestPopup != popup) {
          return;
        }
        if (parent) {
          if (e.type == 'mousedown' && !parent.contains(e.target) && !popup.contains(e.target)) {
            HtmlEditor.closePopup(id, instance, callback);
          }
        } else {
          if (!popup.contains(e.target)) {
            HtmlEditor.closePopup(id, instance, callback);
          }
        }
    };

    if (!HtmlEditor.closePopupsOnScroll) {
        HtmlEditor.closePopupsOnScroll = function (e) {
            for (var i = 0; i < HtmlEditor.popups.length; i++) {
                var p = HtmlEditor.popups[i];
                HtmlEditor.closePopup(p.id, p.instance, p.callback);
            }
            HtmlEditor.popups = [];
        };
        HtmlEditor.popups = [];
    }

    HtmlEditor.popups.push({id, instance, callback});

    document.body.appendChild(popup);
    document.removeEventListener('mousedown', HtmlEditor[id]);
    document.addEventListener('mousedown', HtmlEditor[id]);
    window.removeEventListener('resize', HtmlEditor[id]);
    window.addEventListener('resize', HtmlEditor[id]);

    var p = parent;
    while (p && p != document.body) {
        if (p.scrollWidth > p.clientWidth || p.scrollHeight > p.clientHeight) {
            p.removeEventListener('scroll', HtmlEditor.closePopupsOnScroll);
            p.addEventListener('scroll', HtmlEditor.closePopupsOnScroll);
        }
        p = p.parentElement;
    }

    if (!parent) {
        document.removeEventListener('contextmenu', HtmlEditor[id]);
        document.addEventListener('contextmenu', HtmlEditor[id]);
    }
  },
  closePopup: function (id, instance, callback) {
    var popup = document.getElementById(id);
    if (!popup) return;
    if (popup.style.display == 'none') return;

    if (popup) {
      if (popup.minWidth) {
          popup.style.minWidth = '';
      }

      if (window.chrome && HtmlEditor[id + 'FZL']) {
        HtmlEditor[id + 'FZL'].cell.style.left = HtmlEditor[id + 'FZL'].left;
        HtmlEditor[id + 'FZL'] = null;
      }

      popup.style.display = 'none';
    }
    document.removeEventListener('mousedown', HtmlEditor[id]);
    window.removeEventListener('resize', HtmlEditor[id]);
    HtmlEditor[id] = null;

    if (instance) {
      instance.invokeMethodAsync(callback);
    }

    if (HtmlEditor.activeElement && HtmlEditor.activeElement == document.activeElement || HtmlEditor.activeElement && document.activeElement == document.body) {
        HtmlEditor.activeElement.focus();
        HtmlEditor.activeElement = null;
    }
  },
  togglePopup: function (parent, id, syncWidth, instance, callback) {
    var popup = document.getElementById(id);
    if (!popup) return;
    if (popup.style.display == 'block') {
      HtmlEditor.closePopup(id, instance, callback);
    } else {
      HtmlEditor.openPopup(parent, id, syncWidth, null, null, null, instance, callback);
    }
  },
  destroyPopup: function (id) {
    var popup = document.getElementById(id);
    if (popup) {
      popup.parentNode.removeChild(popup);
    }
    document.removeEventListener('mousedown', HtmlEditor[id]);
  },
  openDialog: function (options, dialogService) {
    HtmlEditor.dialogService = dialogService;
    if (
      document.documentElement.scrollHeight >
      document.documentElement.clientHeight
    ) {
      document.body.classList.add('no-scroll');
    }

    if (options.autoFocusFirstElement) {
        setTimeout(function () {
            var dialogs = document.querySelectorAll('.html-editor-dialog-content');
            if (dialogs.length == 0) return;
            var lastDialog = dialogs[dialogs.length - 1];

            if (lastDialog) {
                var focusable = lastDialog.querySelectorAll('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])');
                var firstFocusable = focusable[0];
                if (firstFocusable) {
                    firstFocusable.focus();
                }
            }
        }, 500);
    }
    if (options.closeDialogOnEsc) {
        document.removeEventListener('keydown', HtmlEditor.closePopupOrDialog);
        document.addEventListener('keydown', HtmlEditor.closePopupOrDialog);
    }
  },
  closeDialog: function () {
    document.body.classList.remove('no-scroll');
  },
  closePopupOrDialog: function (e) {
      e = e || window.event;
      var isEscape = false;
      if ("key" in e) {
          isEscape = (e.key === "Escape" || e.key === "Esc");
      } else {
          isEscape = (e.keyCode === 27);
      }
      if (isEscape && HtmlEditor.dialogService) {
          var popups = document.querySelectorAll('.html-editor-popup,.html-editor-overlaypanel');
          for (var i = 0; i < popups.length; i++) {
              if (popups[i].style.display != 'none') {
                  return;
              }
          }

          HtmlEditor.dialogService.invokeMethodAsync('HtmlEditorDialogService.Close', null);
      }
  },
  getInputValue: function (arg) {
    var input =
      arg instanceof Element || arg instanceof HTMLDocument
        ? arg
        : document.getElementById(arg);
    return input ? input.value : '';
  },
  setInputValue: function (arg, value) {
    var input =
      arg instanceof Element || arg instanceof HTMLDocument
        ? arg
        : document.getElementById(arg);
    if (input) {
      input.value = value;
    }
  },
  readFileAsBase64: function (fileInput, maxFileSize, maxWidth, maxHeight) {
    var calculateWidthAndHeight = function (img) {
      var width = img.width;
      var height = img.height;
      // Change the resizing logic
      if (width > height) {
        if (width > maxWidth) {
          height = height * (maxWidth / width);
          width = maxWidth;
        }
      } else {
        if (height > maxHeight) {
          width = width * (maxHeight / height);
          height = maxHeight;
        }
      }
      return { width, height };
    };
    var readAsDataURL = function (fileInput) {
      return new Promise(function (resolve, reject) {
        var reader = new FileReader();
        reader.onerror = function () {
          reader.abort();
          reject('Error reading file.');
        };
        reader.addEventListener(
          'load',
          function () {
            if (maxWidth > 0 && maxHeight > 0) {
              var img = document.createElement("img");
              img.onload = function (event) {
                // Dynamically create a canvas element
                var canvas = document.createElement("canvas");
                var res = calculateWidthAndHeight(img);
                canvas.width = res.width;
                canvas.height = res.height;
                var ctx = canvas.getContext("2d");
                ctx.drawImage(img, 0, 0, res.width, res.height);
                resolve(canvas.toDataURL(fileInput.type));
              }
              img.src = reader.result;
            } else {
              resolve(reader.result);
            }
          },
          false
          );
        var file = fileInput.files[0];
        if (!file) return;
        if (file.size <= maxFileSize) {
          reader.readAsDataURL(file);
        } else {
          reject('File too large.');
        }
      });
    };

    return readAsDataURL(fileInput);
  },
  destroyResizable: function (ref) {
    if (ref.resizeObserver) {
      ref.resizeObserver.disconnect();
      delete ref.resizeObserver;
    }
    if (ref.resizeHandler) {
      window.removeEventListener('resize', ref.resizeHandler);
      delete ref.resizeHandler;
    }
  },
  createResizable: function (ref, instance) {
    ref.resizeHandler = function () {
      var rect = ref.getBoundingClientRect();

      instance.invokeMethodAsync('Resize', rect.width, rect.height);
    };

    if (window.ResizeObserver) {
      ref.resizeObserver = new ResizeObserver(ref.resizeHandler);
      ref.resizeObserver.observe(ref);
    } else {
      window.addEventListener('resize', ref.resizeHandler);
    }

    var rect = ref.getBoundingClientRect();

    return {width: rect.width, height: rect.height};
  },
  innerHTML: function (ref, value) {
    if (value != undefined) {
      ref.innerHTML = value;
    } else {
      return ref.innerHTML;
    }
  },
  execCommand: function (ref, name, value) {
    if (document.activeElement != ref) {
      ref.focus();
    }
    document.execCommand(name, false, value);
    return this.queryCommands(ref);
  },
  queryCommands: function (ref) {
    return {
      html: ref.innerHTML,
      fontName: document.queryCommandValue('fontName'),
      fontSize: document.queryCommandValue('fontSize'),
      formatBlock: document.queryCommandValue('formatBlock'),
      bold: document.queryCommandState('bold'),
      underline: document.queryCommandState('underline'),
      justifyRight: document.queryCommandState('justifyRight'),
      justifyLeft: document.queryCommandState('justifyLeft'),
      justifyCenter: document.queryCommandState('justifyCenter'),
      justifyFull: document.queryCommandState('justifyFull'),
      italic: document.queryCommandState('italic'),
      strikeThrough: document.queryCommandState('strikeThrough'),
      superscript: document.queryCommandState('superscript'),
      subscript: document.queryCommandState('subscript'),
      unlink: document.queryCommandEnabled('unlink'),
      undo: document.queryCommandEnabled('undo'),
      redo: document.queryCommandEnabled('redo')
    };
  },
  mediaQueries: {},
  mediaQuery: function(query, instance) {
    if (instance) {
      function callback(event) {
          instance.invokeMethodAsync('OnChange', event.matches)
      };
      var query = matchMedia(query);
      this.mediaQueries[instance._id] = function() {
          query.removeListener(callback);
      }
      query.addListener(callback);
      return query.matches;
    } else {
        instance = query;
        if (this.mediaQueries[instance._id]) {
            this.mediaQueries[instance._id]();
            delete this.mediaQueries[instance._id];
        }
    }
  },
  createEditor: function (ref, uploadUrl, paste, instance) {
    ref.inputListener = function () {
      instance.invokeMethodAsync('OnChange', ref.innerHTML);
    };
    ref.selectionChangeListener = function () {
      if (document.activeElement == ref) {
        instance.invokeMethodAsync('OnSelectionChange');
      }
    };
    ref.pasteListener = function (e) {
      var item = e.clipboardData.items[0];

      if (item.kind == 'file') {
        e.preventDefault();

        var xhr = new XMLHttpRequest();
        var data = new FormData();
        data.append("file", item.getAsFile());
        xhr.onreadystatechange = function (e) {
          if (xhr.readyState === XMLHttpRequest.DONE) {
            var status = xhr.status;
            if (status === 0 || (status >= 200 && status < 400)) {
              var result = JSON.parse(xhr.responseText);
              document.execCommand("insertHTML", false, '<img src="' + result.url + '">');
            } else {
              console.log(xhr.responseText);
            }
          }
        }
        instance.invokeMethodAsync('GetHeaders').then(function (headers) {
            xhr.open('POST', uploadUrl, true);
            for (var name in headers) {
              xhr.setRequestHeader(name, headers[name]);
            }
            xhr.send(data);
          });
      } else if (paste) {
        e.preventDefault();
        var data = e.clipboardData.getData('text/html') || e.clipboardData.getData('text/plain');

        instance.invokeMethodAsync('OnPaste', data)
          .then(function (html) {
            document.execCommand("insertHTML", false, html);
          });
      }
    };
    ref.addEventListener('input', ref.inputListener);
    ref.addEventListener('paste', ref.pasteListener);
    document.addEventListener('selectionchange', ref.selectionChangeListener);
    document.execCommand('styleWithCSS', false, true);
  },
  saveSelection: function (ref) {
    if (document.activeElement == ref) {
      var selection = getSelection();
      if (selection.rangeCount > 0) {
        ref.range = selection.getRangeAt(0);
      }
    }
  },
  restoreSelection: function (ref) {
    var range = ref.range;
    if (range) {
      delete ref.range;
      ref.focus();
      var selection = getSelection();
      selection.removeAllRanges();
      selection.addRange(range);
    }
  },
  selectionAttributes: function (selector, attributes) {
    var selection = getSelection();
    var target = selection.focusNode;
    var innerHTML;
    if (target) {
      if (target.nodeType == 3) {
        target = target.parentElement;
      } else {
        target = target.childNodes[selection.focusOffset];
        if (target) {
          innerHTML = target.outerHTML;
        }
      }
      if (target && !target.matches(selector)) {
        target = target.closest(selector);
      }
    }
    return attributes.reduce(function (result, name) {
      if (target) {
        result[name] = target[name];
      }
      return result;
    }, { innerText: selection.toString(), innerHTML: innerHTML });
  },
  destroyEditor: function (ref) {
    if (ref) {
      ref.removeEventListener('input', ref.inputListener);
      ref.removeEventListener('paste', ref.pasteListener);
      document.removeEventListener('selectionchange', ref.selectionChangeListener);
    }
  },
  startDrag: function (ref, instance, handler) {
    ref.mouseMoveHandler = function (e) {
      instance.invokeMethodAsync(handler, { clientX: e.clientX, clientY: e.clientY });
    };
    ref.touchMoveHandler = function (e) {
      if (e.targetTouches[0]) {
        instance.invokeMethodAsync(handler, { clientX: e.targetTouches[0].clientX, clientY: e.targetTouches[0].clientY });
      }
    };
    ref.mouseUpHandler = function (e) {
      HtmlEditor.endDrag(ref);
    };
    document.addEventListener('mousemove', ref.mouseMoveHandler);
    document.addEventListener('mouseup', ref.mouseUpHandler);
    document.addEventListener('touchmove', ref.touchMoveHandler, { passive: true })
    document.addEventListener('touchend', ref.mouseUpHandler, { passive: true });
    return HtmlEditor.clientRect(ref);
  },
  submit: function (form) {
    form.submit();
  },
  clientRect: function (arg) {
    var el = arg instanceof Element || arg instanceof HTMLDocument
        ? arg
        : document.getElementById(arg);
    var rect = el.getBoundingClientRect();
    return { left: rect.left, top: rect.top, width: rect.width, height: rect.height };
  },
  endDrag: function (ref) {
    document.removeEventListener('mousemove', ref.mouseMoveHandler);
    document.removeEventListener('mouseup', ref.mouseUpHandler);
    document.removeEventListener('touchmove', ref.touchMoveHandler)
    document.removeEventListener('touchend', ref.mouseUpHandler);
  },

    openWaiting: function() {
        if (document.documentElement.scrollHeight > document.documentElement.clientHeight) {
            document.body.classList.add('no-scroll');
        }
        if (HtmlEditor.WaitingIntervalId != null) {
            clearInterval(HtmlEditor.WaitingIntervalId);
        }

        setTimeout(function() {
                var timerObj = document.getElementsByClassName('.html-editor-waiting-timer');
                if (timerObj.length == 0) return;
                var timerStart = new Date().getTime();
                HtmlEditor.WaitingIntervalId = setInterval(function() {
                        if (timerObj == null || timerObj[0] == null) {
                            clearInterval(HtmlEditor.WaitingIntervalId);
                        } else {
                            var time = new Date(new Date().getTime() - timerStart);
                            timerObj[0].innerHTML = Math.floor(time / 1000) + "." + Math.floor((time % 1000) / 100);
                        }
                    },
                    100);
            },
            100);
    },
    closeWaiting: function() {
        document.body.classList.remove('no-scroll');
        if (HtmlEditor.WaitingIntervalId != null) {
            clearInterval(HtmlEditor.WaitingIntervalId);
            HtmlEditor.WaitingIntervalId = null;
        }
    }
};
