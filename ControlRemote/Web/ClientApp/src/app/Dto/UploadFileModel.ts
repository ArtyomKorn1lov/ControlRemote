export class UploadFileModel {
    nameFile: string;
    uploadedFile: FormData;

    constructor(_nameFile: string, _uploadedFile: FormData) {
        this.nameFile = _nameFile;
        this.uploadedFile = _uploadedFile;
    }
}