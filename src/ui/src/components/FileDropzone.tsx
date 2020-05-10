import React from 'react'
import { DropzoneArea } from 'material-ui-dropzone'

interface DropzoneProps {
    onChange: (files: File[]) => void
}

export const FileDropzone: React.FC<DropzoneProps> = (props) => {
    return (
        <DropzoneArea
            dropzoneText="Drag and drop images here or click. Allowed up to 5 files, maximum 5 megabytes per file."
            showFileNamesInPreview={true}
            acceptedFiles={['image/*']}
            filesLimit={5}
            maxFileSize={(1024 * 1024 * 5)}
            previewGridProps={{
                container: { spacing: 3, direction: "row", justify: 'center', alignItems: 'center' },
                item: { xs: 'auto' }
            }}
            onChange={props.onChange}
        />
    )
}
