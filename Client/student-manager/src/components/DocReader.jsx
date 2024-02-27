import React, { useState } from 'react';
import DocViewer from "@cyntler/react-doc-viewer";

const DocxReader = () => {

    return (
        <div>
            <DocViewer
                documents={[{ uri: 'https://localhost:9000/public/testDoc.docx' }]}
            />
        </div>
    );
};

export default DocxReader;
