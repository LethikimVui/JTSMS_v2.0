$(document).ready(function () {

    
    toolTip = document.getElementById('myTooltip')
    toolTip.addEventListener('click', copy);
    toolTip.addEventListener('mouseleave', out);
    function copy() {
        debugger
        var copyText = document.getElementById("txt-scriptid");
        copyText.select();
        // copyText.setSelectionRange(0, 99999);
        // navigator.clipboard.writeText(copyText.value);
        document.execCommand("copy");
        var tooltip = document.getElementById("myTooltip");
        tooltip.innerHTML = "Copied";
    }
    function out() {
        var tooltip = document.getElementById("myTooltip");
        tooltip.innerHTML = "Copy";
    }
    script = document.getElementById('txt-script')
    if (script) {
        script.addEventListener('change', Script)
    }
    encrypted = document.getElementById('txt-encrypted')
    if (encrypted) {
        encrypted.addEventListener('change', Encripted)
    }
    function Encripted() {

        var reader = new FileReader(); // define a Reader
        var file = $('#txt-encrypted')[0].files[0]; // get the File obect
        debugger
        if (!file) {
            alert('no encrypted file selected');
            return
        } // check if user selected a file
        else if ((file.size / (1024 * 1024)) > 25) {
            alert('Max file size is 25MB');
            this.value = null;
            return
        }
        reader.onload = function (f) {
            var file_result = this.result; // this == reader, get the loaded file "result"
            var file_wordArr = CryptoJS.lib.WordArray.create(file_result); //convert blob to WordArray , see https://code.google.com/p/crypto-js/issues/detail?id=67
            var sha1_hash = CryptoJS.SHA1(file_wordArr); //calculate SHA1 hash
            var sha1_hash1 = sha1_hash.toString()
            //alert("Calculated SHA1:" + sha1_hash.toString()); //output result
            $('#txt-encrypted').attr('data-hash-encrypted', sha1_hash1)
        };
        reader.readAsArrayBuffer(file); // read the file as ArrayBuffer
    }
    function Script() {
        var reader = new FileReader(); // define a Reader
        var file = $('#txt-script')[0].files[0]; // get the File obect
        if (!file) {
            alert('no script file selected');
            return
        } // check if user selected a file
        reader.onload = function (f) {
            var file_result = this.result; // this == reader, get the loaded file "result"
            var file_wordArr = CryptoJS.lib.WordArray.create(file_result); //convert blob to WordArray , see https://code.google.com/p/crypto-js/issues/detail?id=67
            var sha1_hash = CryptoJS.SHA1(file_wordArr); //calculate SHA1 hash
            var sha1_hash1 = sha1_hash.toString()
            //alert("Calculated SHA1:" + sha1_hash.toString()); //output result
            $('#txt-script').attr('data-hash-script', sha1_hash1)
        };
        reader.readAsArrayBuffer(file); // read the file as ArrayBuffer
    }
})