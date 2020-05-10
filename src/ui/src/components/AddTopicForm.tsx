import React from 'react';
import * as Yup from "yup";
import { TopicsClient, FileParameter } from '../api/ImageboardClient';
import { Box, TextField, useTheme, Grid, Container, Button, CircularProgress, Paper } from '@material-ui/core';
import { Formik } from 'formik';
import { FileDropzone } from './FileDropzone';
import { TextEditor } from './TextEditor';

interface ITopicFormFields {
    boardId: number,
    title?: string,
    text?: string,
    signature?: string,
    attachments?: FileParameter[]
}

export interface AddTopicFormProps {
    boardId: number,
    onSubmitted: (topicId: number) => void
}

const AddTopicSchema = Yup.object().shape<ITopicFormFields>({
    title: Yup.string()
        .min(3, 'Too short')
        .max(64, 'Too long')
        .required('Required'),
    signature: Yup.string()
        .max(32, 'Too long'),
    text: Yup.string()
        .min(10, 'Too short')
        .max(15000, 'Too long')
        .required('Required'),
    boardId: Yup.number().required()
});

export const AddTopicForm: React.FC<AddTopicFormProps> = (props) => {
    const theme = useTheme();

    const initialValues: ITopicFormFields = {
        boardId: props.boardId
    }

    return (
        <Box>
            <Container>
                <Formik
                    initialValues={initialValues}
                    validationSchema={AddTopicSchema}
                    onSubmit={(values, actions) => {
                        const client = new TopicsClient(process.env.REACT_APP_API_URL);

                        client.create(values.boardId, values.title, values.text, values.signature, values.attachments)
                            .then((newTopicId) => {
                                actions.setSubmitting(false);
                                props.onSubmitted(newTopicId);
                            });
                    }}
                >
                    {({ errors, touched, handleChange, values, handleBlur, handleSubmit, handleReset, isSubmitting, }) => (
                        <Paper style={{ margin: theme.spacing(2) }}>
                            <form onSubmit={handleSubmit} style={{ margin: theme.spacing(2) }}>
                                <Grid container direction="column" spacing={3}>
                                    <Grid item xs={12} sm={10} md={6} lg={4}>
                                        <TextField
                                            required
                                            fullWidth
                                            error={(errors.title != null && (touched.title ?? false))}
                                            label="Title"
                                            name="title"
                                            value={values.title}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            helperText={errors.title}
                                            placeholder="Enter title..."
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextEditor
                                            name="text"
                                            label="Text"
                                            required
                                            value={values.text}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            error={(errors.text != null && (touched.text ?? false))}
                                            errors={errors.text}
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={8} md={4} lg={3} xl={3}>
                                        <TextField
                                            fullWidth
                                            error={(errors.signature != null && (touched.signature ?? false))}
                                            label="Signature"
                                            name="signature"
                                            value={values.signature}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            helperText={errors.signature}
                                            placeholder="Enter signature (if you want)"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Box>
                                            <FileDropzone onChange={(files) => {
                                                values.attachments = files.map((file) => {
                                                    const fileParameter: FileParameter = {
                                                        data: file,
                                                        fileName: file.name
                                                    };

                                                    return fileParameter;
                                                })
                                            }} />
                                        </Box>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Box style={{ display: 'flex', alignItems: 'center' }}>
                                            <Button type="submit" disabled={isSubmitting} color="primary" variant="contained" >Add</Button>
                                            {isSubmitting ? <CircularProgress size={24} style={{ marginLeft: theme.spacing(1), marginRight: theme.spacing(1) }} /> : ""}
                                        </Box>
                                    </Grid>
                                </Grid>
                            </form>
                        </Paper>
                    )}
                </Formik>
            </Container>
        </Box >
    )
}