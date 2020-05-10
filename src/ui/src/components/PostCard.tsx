import React, { useState } from 'react';
import { PostDto } from '../api/ImageboardClient';
import { Box, Card, CardContent, CardHeader, Typography, useTheme, CardActions, Button, Grid, Link } from '@material-ui/core';
import { PersonOutline } from '@material-ui/icons';
import { AddPostDialog } from './AddPostDialog';
import FsLightbox from 'fslightbox-react';
const ReactMarkdown = require('react-markdown');

interface PostCardProps {
    data: PostDto,
    onSubmitted?: (postId: number) => void
    showActions?: boolean
}

export const PostCard: React.FC<PostCardProps> = (props) => {
    const theme = useTheme();
    const [postFormIsVisible, setPostFormIsVisible] = useState(false);

    const [lightboxController, setLightboxController] = useState({
        toggler: false,
        slide: 1
    });

    const images = props.data.attachments?.map((attachment) => process.env.REACT_APP_API_ATTACHMENTS_URL + (attachment.filename ?? "")) ?? [];

    function openLightboxOnSlide(number: number) {
        setLightboxController({
            slide: number,
            toggler: !lightboxController.toggler
        })
    }

    return (
        <Card variant="outlined">
            <CardHeader
                style={{ paddingTop: theme.spacing(1), paddingBottom: theme.spacing(0) }}
                subheader={
                    <Box style={{ display: "flex", flexWrap: 'wrap' }}>
                        <PersonOutline style={{ marginRight: 5, width: 18, height: 18 }} />
                        {
                            (props.data.isOp ?? false) ?
                                <Typography style={{ marginRight: 5 }} variant="body2" color="primary" component="span">OP</Typography> :
                                ""
                        }
                        <Typography variant="body2" color="textPrimary" component="span">{(props.data.signature ?? "Anonymous")}</Typography>
                        <Typography title={props.data.created?.local().format("LLL")} variant="body2" component="span">{", " + props.data.created?.fromNow()}</Typography>
                    </Box>
                }
            />
            <CardContent style={{ paddingTop: theme.spacing(2), paddingBottom: theme.spacing(1) }}>
                <Grid container spacing={2}>
                    {props.data.attachments?.map((attachment, index) =>
                        <Grid key={attachment.id} item xs={'auto'}>
                            <Link onClick={() => openLightboxOnSlide(index + 1)}>
                                <img
                                    alt={attachment.originalFilename}
                                    src={process.env.REACT_APP_API_ATTACHMENTS_URL + (attachment.filename ?? "")}
                                    style={{ maxHeight: '200px', maxWidth: '150px' }} />
                                <Typography variant="subtitle2">{attachment.originalFilename}</Typography>
                            </Link>
                        </Grid>
                    )}

                    <Grid item xs={'auto'}>
                        <ReactMarkdown source={props.data.text} />
                    </Grid>
                </Grid>

            </CardContent>

            {
                (props.showActions ?? false) ?
                    <CardActions style={{ paddingTop: theme.spacing(1), paddingBottom: theme.spacing(1) }}>
                        <Button size="small" color="primary" onClick={() => setPostFormIsVisible(true)}>
                            Reply
                        </Button>
                    </CardActions>
                    :
                    ""
            }

            {postFormIsVisible ?
                <AddPostDialog
                    topicId={props.data.topicId ?? 0}
                    parentPost={props.data}
                    open={true}
                    changeVisibilityHandler={setPostFormIsVisible}
                    onSubmitted={(postId) => { setPostFormIsVisible(false); props.onSubmitted?.(postId); }}
                    title="Reply on post"
                /> :
                ""
            }

            <FsLightbox
                toggler={lightboxController.toggler}
                sources={images}
                slide={lightboxController.slide}
                type="image"
            />
        </Card>
    )
}