import React from 'react';
import * as Yup from "yup";
import { PostDto, PostsClient, FileParameter } from '../api/ImageboardClient';
import { Box, TextField, useTheme, Grid, Paper, Button, CircularProgress, FormControlLabel, Checkbox, Typography, Container } from '@material-ui/core';
import { Formik } from 'formik';
import { PostCard } from './PostCard';
import { FileDropzone } from './FileDropzone';
import { TextEditor } from './TextEditor';

interface IPostFormFields {
    topicId: number,
    parentPostId?: number,
    text?: string,
    signature?: string,
    isOp?: boolean,
    attachments?: FileParameter[]
}

export interface AddPostFormProps {
    topicId: number,
    parentPost?: PostDto,
    onSubmitted: (postId: number) => void
}

const AddTopicSchema = Yup.object().shape<IPostFormFields>({
    signature: Yup.string()
        .max(32, 'Too long'),
    text: Yup.string()
        .min(10, 'Too short')
        .max(15000, 'Too long')
        .required('Required'),
    topicId: Yup.number().required(),
    isOp: Yup.bool()
});

export const AddPostForm: React.FC<AddPostFormProps> = (props) => {
    const theme = useTheme();

    const initialValues: IPostFormFields = {
        isOp: false,
        parentPostId: props.parentPost?.id,
        topicId: props.topicId
    }

    return (
        <Box>
            <Container>
                {
                    props.parentPost == null ? "" :
                        <Box style={{ margin: theme.spacing(2), marginBottom: theme.spacing(4) }}>
                            <Typography variant="caption">Source post:</Typography>
                            <PostCard showActions={false} data={props.parentPost} />
                        </Box>
                }

                <Formik
                    initialValues={initialValues}
                    validationSchema={AddTopicSchema}
                    onSubmit={(values, actions) => {
                        const client = new PostsClient(process.env.REACT_APP_API_URL);

                        client.create(values.topicId, values.parentPostId, values.text, values.isOp, values.signature, values.attachments)
                            .then((newPostId) => {
                                actions.setSubmitting(false);
                                props.onSubmitted(newPostId);
                            })
                            .catch((error) => {
                                actions.setSubmitting(false);
                            });
                    }}
                >
                    {({ errors, touched, handleChange, values, handleBlur, handleSubmit, handleReset, isSubmitting, }) => (
                        <Paper style={{ margin: theme.spacing(2) }}>
                            <form onSubmit={handleSubmit} style={{ margin: theme.spacing(2) }}>
                                <Grid container direction="column" spacing={3}>
                                    <Grid item xs={12}>
                                        <TextEditor
                                            name="text"
                                            value={values.text}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            error={(errors.text != null && (touched.text ?? false))}
                                            errors={errors.text}
                                        />
                                        {/* <TextField
                                            variant="outlined"
                                            fullWidth
                                            required
                                            error={(errors.text != null && (touched.text ?? false))}
                                            label="Text"
                                            name="text"
                                            value={values.text}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            helperText={errors.text}
                                            multiline
                                            rows={6}
                                            placeholder="Enter topic text..."
                                        /> */}
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
                                    <Grid item xs={12} sm={8} md={4} lg={3} xl={3}>
                                        <FormControlLabel
                                            control={
                                                <Checkbox
                                                    checked={values.isOp}
                                                    onChange={handleChange}
                                                    name="isOp"
                                                    color="primary"
                                                />
                                            }
                                            label="Is OP"
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